import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Table from '@material-ui/core/Table';
import TableCell from '@material-ui/core/TableCell';
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
  const { resume, details } = props;

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
          {resume.amount.toFixed(2)}
        </Typography>
        <Typography className={classes.title} color="textSecondary" gutterBottom>
                    Saldo acumulado
        </Typography>
        <Typography variant="h5" component="h2">
          {resume.accumulatedAmount.toFixed(2)}
        </Typography>
        <Typography className={classes.titleDetails} color="textSecondary" gutterBottom>
                    Detalhes dos lançamentos
        </Typography>
        <Typography variant="body2" component="div">
          {details && details.items
            ? (
              <Table>
                <tbody>
                  {details.items.map((item) => (
                    <TableRow key={item.id}>
                      <TableCell>{item.description}</TableCell>
                      <TableCell>{item.category}</TableCell>
                      <TableCell align="right">{item.amount.toFixed(2)}</TableCell>
                    </TableRow>
                  ))}
                </tbody>
              </Table>
            )
            : (<span> Nenhum lançamento </span>)}
        </Typography>
      </CardContent>
    </Card>
  );
}
