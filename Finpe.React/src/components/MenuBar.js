import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import { useAuth0 } from "./react-auth0-wrapper";

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

export default function ButtonAppBar(props) {
    const { loading, isAuthenticated, loginWithRedirect, logout, user } = useAuth0();
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        Finpe
                </Typography>
                    {!isAuthenticated && <Button color="inherit" onClick={loginWithRedirect} disabled={loading}>Login</Button>}
                    {isAuthenticated && <Button color="inherit" disabled={loading}>{user ? user.name : 'Loading...'}</Button>}
                    {isAuthenticated && <Button color="inherit" onClick={logout} disabled={loading}>Logout</Button>}
                </Toolbar>
            </AppBar>
        </div>
    )
}
