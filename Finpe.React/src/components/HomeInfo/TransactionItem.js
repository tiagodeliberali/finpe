import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import PropTypes from 'prop-types';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import CheckIcon from '@material-ui/icons/Check';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Fab from '@material-ui/core/Fab';
import AddIcon from '@material-ui/icons/Add';
import { consolidateTransaction, deleteTransaction } from '../../utils/DataProcessor';

const useStyles = makeStyles(() => ({
  button: {
    marginLeft: 10,
  },
  amountTextBox: {
    width: 90,
  },
  amount: {
    fontSize: 18,
    textAlign: 'right',
  },
  pos: {
    fontSize: 12,
    textAlign: 'right',
  },
}));

const formatDate = (dateStr) => {
  const date = new Date(Date.parse(dateStr));

  const weekday = new Array(7);
  weekday[0] = 'Domingo';
  weekday[1] = 'Segunda';
  weekday[2] = 'Terça';
  weekday[3] = 'Quarta';
  weekday[4] = 'Quinta';
  weekday[5] = 'Sexta';
  weekday[6] = 'Sábado';

  return `${weekday[date.getDay()]}, ${date.getDate()}`;
};

const TransactionItem = (props) => {
  const [removed, setRemoved] = useState(false);
  const [newAmount, setNewAmount] = useState(0);
  const [showActions, setShowActions] = useState(false);
  const [confirmAction, setConfirmAction] = useState(null);
  const classes = useStyles();
  const { item, token, allowConsolidate } = props;

  useEffect(() => {
    setNewAmount(item.amount);
  }, [item]);

  const itemInfo = (
    <>
      <Typography className={classes.amount}>
        {item.amount.toFixed(2)}
      </Typography>
      <Typography className={classes.pos} color="textSecondary">
        {formatDate(item.transactionDate)}
      </Typography>
    </>
  );

  const chooseAction = (
    <>
      <IconButton className={classes.button} edge="end" aria-label="comments" onClick={() => setConfirmAction('delete')}>
        <DeleteIcon />
      </IconButton>
      {allowConsolidate && <IconButton className={classes.button} edge="end" aria-label="comments" onClick={() => setConfirmAction('consolidate')}>
        <CheckIcon />
      </IconButton>}
    </>
  );

  const executeConsolidation = () => {
    setRemoved(true);
    consolidateTransaction(token, item, newAmount);
  };

  const confirmPayment = (
    <>
      <TextField
        className={classes.amountTextBox}
        id="amount"
        label="Valor"
        type="number"
        onChange={(e) => setNewAmount(e.target.value)}
        onBlur={(e) => setNewAmount(e.target.value)}
        value={newAmount}
      />
      <Fab size="small" color="secondary" aria-label="add" onClick={executeConsolidation}>
        <AddIcon />
      </Fab>
    </>
  );

  const executeDelete = () => {
    setRemoved(true);
    deleteTransaction(token, item);
  };

  const confirmDelete = (
    <Button variant="contained" color="secondary" onClick={executeDelete}>
      excluir
    </Button>
  );

  const itemAction = () => {
    if (confirmAction === 'delete') {
      return confirmDelete;
    }
    if (confirmAction === 'consolidate') {
      return confirmPayment;
    }
    return chooseAction;
  };

  const toggleActions = () => {
    if (item.description && item.description.startsWith('Budget - ')) {
      return;
    }
    setConfirmAction(null);
    setShowActions(!showActions);
  };

  const result = (
    <ListItem button onClick={toggleActions}>
      <ListItemText primary={item.description} secondary={item.category} />
      <ListItemSecondaryAction>
        {showActions ? itemAction() : itemInfo}
      </ListItemSecondaryAction>
    </ListItem>
  );

  return (removed ? null : result);
};

TransactionItem.propTypes = {
  item: PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  token: PropTypes.string.isRequired,
  allowConsolidate: PropTypes.bool,
};

TransactionItem.defaultProps = {
  allowConsolidate: true,
};

export default TransactionItem;
