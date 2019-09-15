import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';

const useStyles = makeStyles({
    card: {
        minWidth: 275,
    },
    bullet: {
        display: 'inline-block',
        margin: '0 2px',
        transform: 'scale(0.8)',
    },
    title: {
        fontSize: 14,
        margin: '10px 0px 5px',
    },
    titleDetails: {
        fontSize: 14,
        margin: '20px 0px 15px',
    },
    pos: {
        marginBottom: 12,
    },
});

export default function ChartTooltip(props) {
    const classes = useStyles();
    const { resume, details } = props

    return (
        <Card className={classes.card}>
            <CardContent>
                <Typography className={classes.pos} color="textSecondary">
                    {resume.longDate}
                </Typography>
                <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Saldo do dia
                </Typography>
                <Typography variant="h5" component="h2">
                    {resume.amount}
                </Typography>
                <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Saldo acumulado
                </Typography>
                <Typography variant="h5" component="h2">
                    {resume.accumulatedAmount}
                </Typography>
                <Typography className={classes.titleDetails} color="textSecondary" gutterBottom>
                    Detalhes dos lançamentos
                </Typography>
                <Typography variant="body2" component="p">
                    {details && details.items 
                        ? (<Table> {details.items.map((item) => (<TableRow> 
                                    <TableCell>{item.description}</TableCell> 
                                    <TableCell>{item.category}</TableCell> 
                                    <TableCell align="right">{item.amount}</TableCell> 
                                </TableRow>))}
                            </Table>) 
                        : (<span> Nenhum lançamento </span>)}
                </Typography>
            </CardContent>
        </Card>
    );
}