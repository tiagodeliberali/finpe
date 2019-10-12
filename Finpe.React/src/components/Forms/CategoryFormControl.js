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

const CategoryFormControl = (props) => {
  const { handleChange, handleBlur, value } = props;
  const classes = useStyles();

  return (
    <FormControl className={classes.formControl}>
      <InputLabel htmlFor="category">Categoria</InputLabel>
      <Select
        id="category"
        onChange={handleChange}
        onBlur={handleBlur}
        value={value}
        inputProps={{
          name: 'category',
          id: 'category',
        }}
      >
        <MenuItem value="Compras">Compras</MenuItem>
        <MenuItem value="Cuidados pessoais">Cuidados pessoais</MenuItem>
        <MenuItem value="Entrada">Entrada</MenuItem>
        <MenuItem value="Lazer e Eventos">Lazer e Eventos</MenuItem>
        <MenuItem value="Restaurantes">Restaurantes e cafés</MenuItem>
        <MenuItem value="Saúde">Saúde</MenuItem>
        <MenuItem value="Supermercado">Supermercado</MenuItem>
        <MenuItem value="Trabalho">Trabalho</MenuItem>
        <MenuItem value="Transporte">Transporte</MenuItem>
      </Select>
    </FormControl>
  );
};

CategoryFormControl.propTypes = {
  handleChange: PropTypes.func.isRequired,
  handleBlur: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
};

export default CategoryFormControl;
