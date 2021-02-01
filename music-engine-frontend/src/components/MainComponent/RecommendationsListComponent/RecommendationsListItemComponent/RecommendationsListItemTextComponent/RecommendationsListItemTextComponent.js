import React from 'react';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import HelpIcon from '@material-ui/icons/Help';
import Tooltip from '@material-ui/core/Tooltip';

const RecommendationsListItemTextComponent = ({label, text, tooltipText}) => (
    <div>
        <Typography color="textPrimary" component="span">{label}</Typography>
        {
        tooltipText ? <Tooltip title={tooltipText}><HelpIcon fontSize="small" /></Tooltip> : null
        }: {text}

    </div>
);

RecommendationsListItemTextComponent.propTypes = {
  label: PropTypes.string,
  text: PropTypes.string
};

RecommendationsListItemTextComponent.defaultProps = {};

export default RecommendationsListItemTextComponent;
