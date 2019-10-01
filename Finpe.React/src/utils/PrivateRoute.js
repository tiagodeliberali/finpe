// src/components/PrivateRoute.js

import React, { useEffect } from "react";
import { useAuth0 } from "./Auth0Wrapper";

const PrivateRoute = ({ component: Component, path, ...rest }) => {
  const { loading, isAuthenticated, loginWithRedirect } = useAuth0();

  useEffect(() => {
    if (loading || isAuthenticated) {
      return;
    }
    const fn = async () => {
      await loginWithRedirect({
        appState: { targetUrl: path }
      });
    };
    fn();
  }, [loading, isAuthenticated, loginWithRedirect, path]);

  return isAuthenticated === true ? <Component {...rest} /> : null;
};

export default PrivateRoute;