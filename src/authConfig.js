// https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-react for where I got this structured code from

/**
 * const that is utilized for authenticating users via Microsoft Azure
 */
export const msalConfig = {
    auth: {
      clientId: "76422eb2-c015-478f-bf1e-019179282c48",
      authority: "https://login.microsoftonline.com/nikolamotor.com", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
      redirectUri: "http://localhost:3000/", //change this accordingly and also on Azure
    },
    cache: {
      cacheLocation: "sessionStorage", // This configures where your cache will be stored
      storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
    }
  };
  
  // Add scopes here for ID token to be used at Microsoft identity platform endpoints.
  export const loginRequest = {
   scopes: ["User.Read"]
  };
  
  // Add the endpoints here for Microsoft Graph API services you'd like to use.
  export const graphConfig = {
      graphMeEndpoint: "https://graph.microsoft.com"
  };