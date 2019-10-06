import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import List from '@material-ui/core/List';
import ListSubheader from '@material-ui/core/ListSubheader';
import PropTypes from 'prop-types';
import TransactionItem from './TransactionItem';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    backgroundColor: theme.palette.background.paper,
  },
  card: {
    marginBotton: 20,
  },
}));

const formatDate = ({ month, year }) => {
  const monthName = new Array(7);
  monthName[0] = 'Janeiro';
  monthName[1] = 'Fevereiro';
  monthName[2] = 'Março';
  monthName[3] = 'Abril';
  monthName[4] = 'Maio';
  monthName[5] = 'Junho';
  monthName[6] = 'Julho';
  monthName[7] = 'Agosto';
  monthName[8] = 'Setembro';
  monthName[9] = 'Outubro';
  monthName[10] = 'Novembro';
  monthName[11] = 'Dezembro';

  return `Transações de ${monthName[month - 1]} de ${year}`;
};

const buildTableData = (setData, setTitle, data) => {
  setData(data.lines);
  setTitle(formatDate(data.yearMonth));
};


const NextTransactions = (props) => {
  const [transactions, setTransactions] = useState([]);
  const [title, setTitle] = useState('');
  const classes = useStyles();
  const { data, token } = props;

  useEffect(() => {
    if (data && data[0]) {
      buildTableData(setTransactions, setTitle, data[0]);
    }
  }, [data]);

  return (
    <Card className={classes.card}>
      <List subheader={<ListSubheader>{title}</ListSubheader>} className={classes.root}>
        {transactions
          .filter((item) => !('difference' in item))
          .map((item) => (
            <TransactionItem
              key={JSON.stringify(item)}
              item={item}
              token={token}
            />
          ))}
      </List>
    </Card>
  );
};

NextTransactions.propTypes = {
  data: PropTypes.array.isRequired, // eslint-disable-line react/forbid-prop-types
  token: PropTypes.string.isRequired,
};

export default NextTransactions;
