import React from 'react';
import ReactDOM from 'react-dom';
import { init as sentryInit } from '@sentry/browser';
import App from './App';
import { Auth0Provider } from './utils/Auth0Wrapper';

sentryInit({
  dsn: 'https://e1956abeb17d41e8953a942ebc4f3499@sentry.io/1770791',
  environment: process.env.BRANCH,
  release: process.env.APP_VERSION,
});

// A function that routes the user to the right place
// after login
const onRedirectCallback = (appState) => {
  window.history.replaceState(
    {},
    document.title,
    appState && appState.targetUrl
      ? appState.targetUrl
      : window.location.pathname,
  );
};

ReactDOM.render(
  <Auth0Provider
    domain={process.env.AUTH_DOMAIN}
    client_id={process.env.AUTH_CLIENT_ID}
    audience={process.env.AUTH_AUDIENCE}
    redirect_uri={window.location.origin}
    onRedirectCallback={onRedirectCallback}
  >
    <App />
  </Auth0Provider>,
  document.getElementById('root'),
);
