import {DndProvider} from "react-dnd";
import {HTML5Backend} from "react-dnd-html5-backend"

import Navbar from "./Navbar";
import History from "./pages/History";
import Home from "./pages/Home";
import AppStyle from "./css/app.module.css"
import { useIsAuthenticated } from "@azure/msal-react";

import Axios from 'axios'
import { useEffect, useState } from 'react'

/**
 * The main file that houses the application
 * @returns the application with the navbar and the correct page
 */
function App() {

  const isAuthenticated = useIsAuthenticated();
  
  const [data, setData] = useState([]);

  // gets all the stations
  useEffect(() => {
    getData();
  }, []);

  // called by useEffect to get the data and set the current state of data as the axios response
  const getData = () => {
    Axios.get(`${process.env.REACT_APP_API_SERVICE_STATION}`).then(
        (response) => {
          setData(response.data)
        }
      )
      .catch(error => console.log(error))
  }

  let component
  switch(window.location.pathname) {
    case "/":
      component = <Home data={data} />
      break;
    case "/history":
      component = <History data={data}/>
      break;
    default:
      component = <Home data={data}/>
      break;
  }
  return (
    <>
      <Navbar />
      { isAuthenticated ? <DndProvider backend={HTML5Backend}>
        {component}
      </DndProvider> : 
      <h1 className={AppStyle.logout}>You are currently signed out. Sign in to view content.</h1>}
    </>
  );
}

export default App;
