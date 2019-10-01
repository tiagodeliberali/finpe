import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';

const useStyles = makeStyles(theme => ({
    card: {
    },
    rootGrid: {
      flexGrow: 1,
    },
    bullet: {
      display: 'inline-block',
      margin: '0 2px',
      transform: 'scale(0.8)',
    },
    title: {
      fontSize: 16,
    },
    subtitle: {
      fontSize: 14,
    },
    amount: {
        fontSize: 18,
      },
    pos: {
        fontSize: 12,
    },
}));

const buildTableData = (setData, data) => {
    setData(data.lines)
}

const formatDate = (dateStr) => {
    const date = new Date(Date.parse(dateStr))

    const weekday = new Array(7);
    weekday[0] =  "Domingo";
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
    <Grid container className={classes.rootGrid} spacing={2}>
      {transactions.map(item => (
          <Grid item xs={12} key={item.category}>
            <Card className={classes.card}>
                <Grid container className={classes.rootGrid} spacing={2}>
                    <Grid item xs={8} key={item.category}>
                        <CardContent>
                        <Typography className={classes.title} color="textPrimary" gutterBottom>
                            {item.description}
                        </Typography>
                        <Typography className={classes.subtitle} color="textSecondary" gutterBottom>
                            {item.category}
                        </Typography>
                        </CardContent>
                    </Grid>
                    <Grid item xs={4} key={item.category}>
                        <CardContent>
                        <Typography className={classes.amount}>
                            {item.amount.toFixed(2)}
                        </Typography>
                        <Typography className={classes.pos} color="textSecondary">
                            {formatDate(item.transactionDate)}
                        </Typography>
                        </CardContent>
                    </Grid>
                </Grid>
          </Card>
        </Grid>
        ))}
    </Grid>
  );
}