# WeatherApps
This application uses React frontend and .net core 3.1 backend, the weather API from openweathermap.org

# Backend Run Instruction
For debugging purposes, you can open the backend project file and replace the WeatherApi API Key in appsettings.Development.json. For API Key, you can get it from openweathermap.org<br>
The current environment is just development<br>
Simply run the project, and it will open swagger UI for the available endpoint

# Frontend Run Instruction
For frontend, you only need to configure the REACT_APP_API_URL in .env in root folder of frontend. You need to set the port same with the backend API port<br>
Run npm install for installing the modules <br>
Start the application with simple comment npm start, the app will open in your browser.
