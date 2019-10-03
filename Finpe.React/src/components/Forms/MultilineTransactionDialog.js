import React from 'react';
import PropTypes from 'prop-types';
import Dialog from '@material-ui/core/Dialog';
import TransactionForm from './TransactionForm';

function MultilineTransactionDialog(props) {
  const { onClose, open, parentId } = props;

  const handleClose = () => {
    onClose();
  };

  return (
    <Dialog onClose={handleClose} aria-labelledby="simple-dialog-title" open={open}>
      <TransactionForm isMultiline parentId={parentId} />
    </Dialog>
  );
}

MultilineTransactionDialog.propTypes = {
  onClose: PropTypes.func.isRequired,
  open: PropTypes.bool.isRequired,
  parentId: PropTypes.number.isRequired,
};

export default MultilineTransactionDialog;
