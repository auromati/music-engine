import React from 'react';
import PropTypes from 'prop-types';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import RecommendationsListItemTextComponent from './RecommendationsListItemTextComponent/RecommendationsListItemTextComponent';
import { Link, Typography } from '@material-ui/core';

const RecommendationsListItemComponent = ({recommendation}) => (
  <React.Fragment>
    <ListItemAvatar>
      <Avatar 
        src={recommendation.imagePath} 
        style={{height: '120px', width: '120px', marginRight: '0.8em'}} />
    </ListItemAvatar>
    <ListItemText
      primary={
      <Link href={recommendation.url}>
        <Typography variant="h6">{recommendation.title}</Typography>
      </Link>}
      secondary={
        <React.Fragment>
          <RecommendationsListItemTextComponent
            label="Artist"
            text={recommendation.artist}
          >
          </RecommendationsListItemTextComponent>
          <RecommendationsListItemTextComponent
            label="Location"
            text={recommendation.location}
          >
          </RecommendationsListItemTextComponent>
          <RecommendationsListItemTextComponent
            label="Release date"
            text={recommendation.releaseDate}
          >
          </RecommendationsListItemTextComponent>
          <RecommendationsListItemTextComponent
            label="Tags"
            text={recommendation.tags.join(', ')}
          >
          </RecommendationsListItemTextComponent>
        </React.Fragment>
      }
    />
  </React.Fragment>

);

RecommendationsListItemComponent.propTypes = {
  recommendation: PropTypes.object
};

RecommendationsListItemComponent.defaultProps = {};

export default RecommendationsListItemComponent;
