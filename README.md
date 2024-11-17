![InvestTrackr Logo](https://github.com/iamjessee/InvestTrackr/blob/master/logo.png)

**Real-time stock data tracker and portfolio management tool.**

## Description

InvestTrackr is a stock tracking and portfolio management tool that fetches real-time stock data, stores it in a database, and allows users to monitor their investments in one place.

## Features

- 🕒 **Real-time stock data fetching** from external APIs.
- 💼 **Portfolio tracking** to monitor your investments.
- 📊 **Database storage** for stock information.
- ⚡ **Simple user-friendly interface** for quick access to data.

## Technologies Used

- **Backend:** ASP.NET Core, C#
- **Database:** SQL Server
- **Frontend:** React, TypeScript
- **API:** FMP Stock API
- **Authentication:** JWT for user authentication

## To run this project locally

1. **Clone the repository:**

- git clone https://github.com/iamjessee/InvestTrackr.git

2. **Navigate to the project directory:**

- cd InvestTrackr

3. **Install necessary dependencies:**
- npm install # For front-end dependencies
- dotnet restore # For backend dependencies

4. **Set up the database connections:**

- Edit the appsettings.json file to match your local configuration (e.g., connection string, API keys).

## What I Learned

- Integrating external APIs with C#
- Handling real-time data fetching and processing
- Implementing user authentication using JWT
- Using TypeScript in React to build the front end

## Future Improvements / To-Do

- 🔧 Need to finish migrating API calls from front end to backend
- ⚙️ Fix some bugs with front end

![InvestTrackr Demo](https://raw.githubusercontent.com/iamjessee/InvestTrackr/master/INVESTTRACKRdemo.gif)
