import React from 'react';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';

const RecommendationsListItemTextComponent = ({label, text}) => (
    <div>
        <Typography color="textPrimary" component="span">{label}:</Typography> {text}
    </div>
);

RecommendationsListItemTextComponent.propTypes = {
  label: PropTypes.string,
  text: PropTypes.string
};

RecommendationsListItemTextComponent.defaultProps = {};

export default RecommendationsListItemTextComponent;
