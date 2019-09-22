import React from 'react';
import { makeStyles, useTheme } from '@material-ui/core/styles';

import Hidden from '@material-ui/core/Hidden';
import Drawer from '@material-ui/core/Drawer';
import Divider from '@material-ui/core/Divider';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import HomeIcon from '@material-ui/icons/Home';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import LockIcon from '@material-ui/icons/Lock';
import LockOpenIcon from '@material-ui/icons/LockOpen';
import { useAuth0 } from "./react-auth0-wrapper";
import { Link } from "@reach/router";

const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
    drawer: {
        [theme.breakpoints.up('sm')]: {
            width: drawerWidth,
            flexShrink: 0,
        },
    },
    appBar: {
        marginLeft: drawerWidth,
        [theme.breakpoints.up('sm')]: {
            width: `calc(100% - ${drawerWidth}px)`,
        },
    },
    menuButton: {
        marginRight: theme.spacing(2),
        [theme.breakpoints.up('sm')]: {
            display: 'none',
        },
    },
    toolbar: theme.mixins.toolbar,
    drawerPaper: {
        width: drawerWidth,
    },
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
    },
    title: {
        flexGrow: 1,
    },
}));

const AdapterLink = React.forwardRef((props, ref) => (
    <Link to={ref} {...props} />
));

export default function ButtonAppBar(props) {
    const { loading, isAuthenticated, loginWithRedirect, logout, user } = useAuth0();
    const classes = useStyles();
    const theme = useTheme();
    const [mobileOpen, setMobileOpen] = React.useState(false);
    const { container } = props;

    function handleDrawerToggle() {
        setMobileOpen(!mobileOpen);
    }

    const drawer = (
        <div>
            <div className={classes.toolbar} />
            <Divider />
            <List>
                {!isAuthenticated && <ListItem button key="Login" onClick={loginWithRedirect} disabled={loading}>
                    <ListItemIcon><LockIcon /></ListItemIcon>
                    <ListItemText primary="Login" />
                </ListItem>}
                {isAuthenticated && <ListItem button key="Logout" onClick={logout} disabled={loading}>
                    <ListItemIcon><LockOpenIcon /></ListItemIcon>
                    <ListItemText primary="Logout" />
                </ListItem>}
            </List>
            <Divider />
            <List>
                <ListItem button key="home" component={AdapterLink} to="/">
                    <ListItemIcon><HomeIcon /></ListItemIcon>
                    <ListItemText primary="Início" />
                </ListItem>
                <ListItem button key="add-recurrency" component={AdapterLink} to="/add-recurrency">
                    <ListItemIcon><AddCircleOutlineIcon /></ListItemIcon>
                    <ListItemText primary="Conta Recorrente" />
                </ListItem>
                <ListItem button key="add-budget" component={AdapterLink} to="/add-budget">
                    <ListItemIcon><AddCircleOutlineIcon /></ListItemIcon>
                    <ListItemText primary="Budget" />
                </ListItem>
            </List>
        </div>
    );

    return (
        <div>
            <AppBar position="fixed" className={classes.appBar}>
                <Toolbar>
                    <IconButton edge="start" className={classes.menuButton} onClick={handleDrawerToggle} color="inherit" aria-label="menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        Finpe
                    </Typography>
                </Toolbar>
            </AppBar>
            <nav className={classes.drawer} aria-label="finpe actions">
                <Hidden smUp implementation="css">
                    <Drawer
                        container={container}
                        variant="temporary"
                        anchor={theme.direction === 'rtl' ? 'right' : 'left'}
                        open={mobileOpen}
                        onClose={handleDrawerToggle}
                        classes={{
                            paper: classes.drawerPaper,
                        }}
                        ModalProps={{
                            keepMounted: true, // Better open performance on mobile.
                        }}
                    >
                        {drawer}
                    </Drawer>
                </Hidden>
                <Hidden xsDown implementation="css">
                    <Drawer
                        classes={{
                            paper: classes.drawerPaper,
                        }}
                        variant="permanent"
                        open
                    >
                        {drawer}
                    </Drawer>
                </Hidden>
            </nav>
        </div>
    )
}
