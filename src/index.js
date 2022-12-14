import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

import { PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";
import { msalConfig } from "./authConfig";


/**
 * Components that are utilized by React to start via "npm start"
 */
const msalInstance = new PublicClientApplication(msalConfig);

const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
    <MsalProvider instance={msalInstance}>
        <App />
    </MsalProvider>
);

