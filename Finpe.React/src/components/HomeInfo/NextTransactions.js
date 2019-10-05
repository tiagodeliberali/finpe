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

const buildTableData = (setData, data) => {
  setData(data.lines);
};


const NextTransactions = (props) => {
  const [transactions, setTransactions] = useState([]);
  const classes = useStyles();
  const { data } = props;

  useEffect(() => {
    if (data && data[0]) {
      buildTableData(setTransactions, data[0]);
    }
  }, [data]);

  return (
    <Card className={classes.card}>
      <List subheader={<ListSubheader>Próximas transações</ListSubheader>} className={classes.root}>
        {transactions.map((item) => <TransactionItem key={JSON.stringify(item)} item={item} />)}
      </List>
    </Card>
  );
};

NextTransactions.propTypes = {
  data: PropTypes.array.isRequired, // eslint-disable-line react/forbid-prop-types
};

export default NextTransactions;
