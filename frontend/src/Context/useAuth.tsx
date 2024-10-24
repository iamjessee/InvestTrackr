import { createContext, useEffect, useState } from "react";
import { UserProfile } from "../Models/User";
import { useNavigate } from "react-router-dom";
import { loginAPI, registerAPI } from "../Services/AuthService";
import { toast } from "react-toastify";
import React from "react";
import axios from "axios";
import { PortfolioGet } from "../Models/Portfolio";
import { portfolioGetAPI } from "../Services/PortfolioService";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  portfolioValues: PortfolioGet[];
  setPortfolioValues: (values: PortfolioGet[]) => void;
  registerUser: (email: string, username: string, password: string) => void;
  loginUser: (username: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
  resetPortfolio: () => void;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[]>([]);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }
    setIsReady(true);
  }, []);

  // Fetch portfolio whenever user or token changes
  useEffect(() => {
    const fetchPortfolio = async () => {
      if (user && token) {
        try {
          const res = await portfolioGetAPI();
          if (res?.data) {
            setPortfolioValues(res.data);
          } else {
            toast.error("No portfolio data found");
          }
        } catch (error) {
          setPortfolioValues([]);
          toast.error("Failed to fetch portfolio");
        }
      } else {
        resetPortfolio();
      }
    };

    fetchPortfolio();
  }, [user, token]);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ) => {
    await registerAPI(email, username, password)
      .then((res) => {
        if (res) {
          localStorage.setItem("token", res?.data.token);
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res?.data.token!);
          setUser(userObj!);
          toast.success("Registration Success!");
          navigate("/search");
        }
      })
      .catch(() => toast.warning("Server error occurred"));
  };

  const loginUser = async (username: string, password: string) => {
    await loginAPI(username, password)
      .then((res) => {
        if (res) {
          localStorage.setItem("token", res?.data.token);
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res?.data.token!);
          setUser(userObj!);
          toast.success("Login Success!");
          navigate("/search");
        }
      })
      .catch(() => toast.warning("Server error occurred"));
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken(null);
    resetPortfolio();
    navigate("/login");
  };

  const resetPortfolio = () => {
    setPortfolioValues([]);
  };

  return (
    <UserContext.Provider
      value={{
        loginUser,
        user,
        token,
        logout,
        isLoggedIn,
        registerUser,
        resetPortfolio,
        portfolioValues,
        setPortfolioValues,
      }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);

export default UserContext;
