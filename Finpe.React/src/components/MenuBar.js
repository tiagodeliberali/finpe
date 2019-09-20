import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import createAuth0Client from '@auth0/auth0-spa-js';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
    },
    menuButton: {
        marginRight: theme.spacing(2),
    },
    title: {
        flexGrow: 1,
    },
}));

let auth0 = undefined;
const configureClient = (setState) => {
    return createAuth0Client({
        domain: process.env.AUTH_DOMAIN,
        client_id: process.env.AUTH_CLIENT_ID,
        audience: process.env.AUTH_AUDIENCE
    })
    .then(result => {
        auth0 = result;
        setState(true);
        return result;
    })
    .then(() => loadDataFromQuerystring());
}

const loadDataFromQuerystring = () => {
    const query = window.location.search;
    if (query.includes("code=") && query.includes("state=")) {
        return auth0.handleRedirectCallback()
            .then(() => window.history.replaceState({}, document.title, "/"));
    }
    return Promise.resolve()
}

const isAuthenticated = (setState) => {
    auth0.isAuthenticated()
        .then(authResult => setState(authResult));
}

const getUserInfo = (setState) => {
    auth0.getUser()
        .then(authResult => setState(authResult));
}

const getToken = (setState) => {
    auth0.getTokenSilently()
        .then(authResult => {
            localStorage.setItem('jwt_token', authResult);
            setState(true)
        });
}

const login = () => {
    return auth0.loginWithRedirect({
        redirect_uri: window.location.origin
    });
};

export default function ButtonAppBar(props) {
    const [canLoggin, setCanLoggin] = useState(false);
    const [loggedIn, setLoggedIn] = useState(false);
    const [userInfo, setUserInfo] = useState({});
    const [token, setToken] = useState(false);
    const classes = useStyles();

    useEffect(() => {
        !canLoggin && configureClient(setCanLoggin)
            .then(() => isAuthenticated(setLoggedIn))
            .then(() => getUserInfo(setUserInfo))
            .then(() => getToken(setToken));
    });

    return (
        <div className={classes.root}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        Finpe {!token ? 'carregando token...' : ''}
                </Typography>
                    <Button color="inherit" onClick={login} disabled={!canLoggin}>{loggedIn && userInfo ? userInfo.name : 'Login'}</Button>
                </Toolbar>
            </AppBar>
        </div>
    )
}
