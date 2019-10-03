import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import PropTypes from 'prop-types';

const useStyles = makeStyles(() => ({
  formControl: {
    minWidth: 150,
  },
}));

const ImportanceFormControl = (props) => {
  const { handleChange, handleBlur, value } = props;
  const classes = useStyles();

  return (
    <FormControl className={classes.formControl}>
      <InputLabel htmlFor="importance">Importância</InputLabel>
      <Select
        id="importance"
        onChange={handleChange}
        onBlur={handleBlur}
        value={value}
        inputProps={{
          name: 'importance',
          id: 'importance',
        }}
      >
        <MenuItem value={0}>NotDefined</MenuItem>
        <MenuItem value={1}>WeLikeIt</MenuItem>
        <MenuItem value={2}>CanBeCut</MenuItem>
        <MenuItem value={3}>HardToCut</MenuItem>
        <MenuItem value={99}>Essential</MenuItem>
      </Select>
    </FormControl>
  );
};

ImportanceFormControl.propTypes = {
  handleChange: PropTypes.func.isRequired,
  handleBlur: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
};

export default ImportanceFormControl;
