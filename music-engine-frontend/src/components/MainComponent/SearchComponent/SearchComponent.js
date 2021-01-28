import React, { useState } from 'react';
import PropTypes from 'prop-types';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
  textField: {
    marginTop: '1.5em'
  },
  button: {
    marginTop: '0.5em'
  }
}));

const SearchComponent = ({recommendationsTriggered}) => {
  const classes = useStyles();
  
  const [description, setDescription] = useState('');
  const handleDescriptionChange = (event) => setDescription(event.target.value);
  const handleClick = () => recommendationsTriggered(description);

  return (
    <div>
      <TextField
        label="Music description"
        multiline
        rowsMax={4}
        value={description}
        onChange={handleDescriptionChange}
        variant="outlined"
        fullWidth={true}
        className={classes.textField}
      />
      <Button 
        onClick={handleClick} 
        variant="contained" 
        color="primary" 
        fullWidth={true}
        className={classes.button}>Find Recommendations</Button>
    </div>
  );
};

SearchComponent.propTypes = {
  recommendationsTriggered: PropTypes.func
};

SearchComponent.defaultProps = {};

export default SearchComponent;
