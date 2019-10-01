import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListSubheader from '@material-ui/core/ListSubheader';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        maxWidth: 360,
        backgroundColor: theme.palette.background.paper,
    },
    card: {
        marginTop: 20,
    },
    title: {
        fontSize: 16,
    },
    subtitle: {
        fontSize: 14,
    },
    amount: {
        fontSize: 18,
        textAlign: "right"
    },
    pos: {
        fontSize: 12,
        textAlign: "right"
    },
}));

const buildTableData = (setData, data) => {
    setData(data.lines)
}

const formatDate = (dateStr) => {
    const date = new Date(Date.parse(dateStr))

    const weekday = new Array(7);
    weekday[0] = "Domingo";
    weekday[1] = "Segunda";
    weekday[2] = "Terça";
    weekday[3] = "Quarta";
    weekday[4] = "Quinta";
    weekday[5] = "Sexta";
    weekday[6] = "Sábado";

    return `${weekday[date.getDay()]}, ${date.getDate()}`
}

export default function NextTransactions(props) {
    const [transactions, setTransactions] = useState([]);
    const classes = useStyles();
    const { data } = props

    useEffect(() => {
        data && data.result && data.result[0] && buildTableData(setTransactions, data.result[0])
    }, [data]);

    return (
        <Card className={classes.card}>
            <List subheader={<ListSubheader>Próximas transações</ListSubheader>} className={classes.root}>
                {transactions.map(item => (
                    <ListItem>
                        <ListItemText primary={item.description} secondary={item.category} />
                        <ListItemSecondaryAction>
                            <React.Fragment>
                                <Typography className={classes.amount}>
                                    {item.amount.toFixed(2)}
                                </Typography>
                                <Typography className={classes.pos} color="textSecondary">
                                    {formatDate(item.transactionDate)}
                                </Typography>
                            </React.Fragment>
                        </ListItemSecondaryAction>
                    </ListItem>
                ))}
            </List>
        </Card>
    );
}