import React from "react";
import Button from "react-bootstrap/Button";
import { useEffect, useState } from "react";

import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import Axios from 'axios'

/**
 * Responsible for handling the logout procedure for when the user clicks the button to sign out.
 * @param {*} instance 
 */
function handleLogout(instance) {
    instance.logoutRedirect().catch(e => {
        console.error(e);
    });
}

/**
 * Renders a button which, when selected, will redirect the page to the logout prompt
 */
export const SignOutButton = (props) => {
    const { instance } = useMsal();
    
    const [people, setPeople] = useState([]);

    useEffect(() => {
        getPeople();
    }, []);

    const getPeople = async () => {
        await Axios.get(`${process.env.REACT_APP_API_SERVICE_PERSON}`).then(
            (response) => {
                var listOfPeople = []
                for (var key in response.data) {
                    listOfPeople.push(response.data[key].email)
                }
                setPeople(listOfPeople);
            })
            .catch(error => console.log(error))
    }

    const msal = useMsal();
    const isAuthenticated = useIsAuthenticated();

    if (isAuthenticated && people.length !== 0 && !people.includes(msal.accounts[0].username)) {
        //Adding to the database
        var x = msal.accounts[0].name.split(" ");
        Axios.post(`${process.env.REACT_APP_API_SERVICE_PERSON}`, {
            email: msal.accounts[0].username,
            fName: x[0],
            lName: x[1]
        })
    } else {
        // Do nothing!
        // User is already in DB or the list is empty
    }

    return (
        <Button variant="secondary" className="ml-auto" onClick={() => handleLogout(instance)}>Sign out</Button>
    );
}