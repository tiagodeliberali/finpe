import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';

const useStyles = makeStyles(theme => ({
    formControl: {
      minWidth: 150,
    },
  }));

export default function CategoryFormControl(props) {
    const { handleChange, handleBlur, value } = props
    const classes = useStyles();

    return (<FormControl className={classes.formControl}>
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
            <MenuItem value={"Assinaturas e Serviços"}>Assinaturas e Serviços</MenuItem>
            <MenuItem value={"Cartões"}>Cartões</MenuItem>
            <MenuItem value={"Compras"}>Compras</MenuItem>
            <MenuItem value={"Cuidados pessoais"}>Cuidados pessoais</MenuItem>
            <MenuItem value={"Dívidas"}>Dívidas</MenuItem>
            <MenuItem value={"Doação"}>Doação</MenuItem>
            <MenuItem value={"Educação"}>Educação</MenuItem>
            <MenuItem value={"Empréstimo"}>Empréstimo</MenuItem>
            <MenuItem value={"Entrada"}>Entrada</MenuItem>
            <MenuItem value={"Habitação"}>Habitação</MenuItem>
            <MenuItem value={"Hobbies"}>Hobbies</MenuItem>
            <MenuItem value={"Lazer e Eventos"}>Lazer e Eventos</MenuItem>
            <MenuItem value={"Restaurantes e cafés"}>Restaurantes e cafés</MenuItem>
            <MenuItem value={"Saldo"}>Saldo</MenuItem>
            <MenuItem value={"Saúde"}>Saúde</MenuItem>
            <MenuItem value={"Supermercado"}>Supermercado</MenuItem>
            <MenuItem value={"Transporte"}>Transporte</MenuItem>
        </Select>
    </FormControl>)
}














