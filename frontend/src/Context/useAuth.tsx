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

  // Load user and token from localStorage on first render
  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    const storedToken = localStorage.getItem("token");

    if (storedUser && storedToken) {
      setUser(JSON.parse(storedUser));
      setToken(storedToken);

      // Set axios Authorization header on first load if token is present
      axios.defaults.headers.common["Authorization"] = `Bearer ${storedToken}`;
    }

    setIsReady(true); // Indicate that initial setup is complete
  }, []);

  // Update Authorization header automatically whenever token changes
  useEffect(() => {
    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      delete axios.defaults.headers.common["Authorization"];
    }
  }, [token]);

  // Fetch portfolio only if isReady is true and token is set
  useEffect(() => {
    const fetchPortfolio = async () => {
      if (token) {
        // Only fetch if token is present
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

    // Fetch portfolio after initial setup (isReady) and only if token is present
    if (isReady && token) {
      fetchPortfolio();
    }
  }, [isReady, token]);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ) => {
    await registerAPI(email, username, password)
      .then((res) => {
        if (res) {
          // Store token and user details after successful registration
          localStorage.setItem("token", res?.data.token);
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res?.data.token!);
          setUser(userObj!);

          // Immediately set axios Authorization header
          axios.defaults.headers.common[
            "Authorization"
          ] = `Bearer ${res?.data.token}`;

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
          // Store token and user details after successful login
          localStorage.setItem("token", res?.data.token);
          const userObj = {
            userName: res?.data.userName,
            email: res?.data.email,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res?.data.token!);
          setUser(userObj!);

          // Immediately set axios Authorization header
          axios.defaults.headers.common[
            "Authorization"
          ] = `Bearer ${res?.data.token}`;

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
    // Remove token and user details from localStorage
    localStorage.removeItem("token");
    localStorage.removeItem("user");

    // Clear user and token state, reset portfolio, and remove Authorization header
    setUser(null);
    setToken(null);
    delete axios.defaults.headers.common["Authorization"];
    resetPortfolio();
    navigate("/login");
  };

  const resetPortfolio = () => {
    setPortfolioValues([]); // Reset portfolio state
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
