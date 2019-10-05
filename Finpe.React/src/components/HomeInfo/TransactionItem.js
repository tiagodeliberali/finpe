import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import PropTypes from 'prop-types';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import CheckIcon from '@material-ui/icons/Check';
import Button from '@material-ui/core/Button';


const useStyles = makeStyles(() => ({
  button: {
    marginLeft: 10,
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
  const [showActions, setShowActions] = useState(false);
  const [confirmAction, setConfirmAction] = useState(null);
  const classes = useStyles();
  const { item } = props;

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
      <IconButton className={classes.button} edge="end" aria-label="comments" onClick={() => setConfirmAction('consolidate')}>
        <CheckIcon />
      </IconButton>
    </>
  );

  const confirmPayment = (
    <Button variant="contained" color="primary">
        pagar
    </Button>
  );

  const confirmDelete = (
    <Button variant="contained" color="secondary">
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
    setConfirmAction(null);
    setShowActions(!showActions);
  };

  return (
    <ListItem button onClick={toggleActions}>
      <ListItemText primary={item.description} secondary={item.category} />
      <ListItemSecondaryAction>
        {showActions ? itemAction() : itemInfo}
      </ListItemSecondaryAction>
    </ListItem>
  );
};

TransactionItem.propTypes = {
  item: PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

export default TransactionItem;
