import React from 'react'

/**
 * Simply displays the name of the user that is logged in.
 * @param {*} props 
 * @returns the welcome message found on the left side of the Navbar.
 */
export default function Welcome(props) {
  return (
    <h1>Welcome, {props.name}</h1>
  )
}
