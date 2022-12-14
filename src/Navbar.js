import React from 'react'
import Welcome from './components/Welcome'
import "./css/navbar.css"
import Logo from "./images/2560px-Nikola_White_logo.svg.png"

import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignInButton } from "./components/SignInButton";
import { SignOutButton } from './components/SignOutButton';

/**
 * Navbar component at the top of the web application.
 * Has buttons for the homepage, history page, and signing in/out of
 * the application.
 * @returns the navbar component that is located at the top of the webpage
 */
export default function Navbar() {
  const isAuthenticated = useIsAuthenticated();
  const msal = useMsal();
  
  if (msal.accounts.length !== 0) {
    //there will only ever be 1 key in this list
    var name = msal.accounts[0].name
  }

  return (
    <nav className="nav">
        { isAuthenticated ? <Welcome name={name}/> : <h1>Please Sign in!</h1> } 
        <img src={Logo} alt="Logo" />
        <ul>
          {
            /*
              Make adjustments to this later so href is not used.
              Href causes rerendering which is not ideal for how
              the application saves the user email and name to 
              SQL Server. Ideally it is done like this: 
              https://v5.reactrouter.com/web/example/basic
              utilizing "react-router-dom".
            */
          }
            <li>
                <a href="/">Home</a>
            </li>
            <li>
                <a href="/history">History</a>
            </li>
            <li>
                { isAuthenticated ? <SignOutButton/> : <SignInButton/> }
            </li>
        </ul>
    </nav>
  )
}
