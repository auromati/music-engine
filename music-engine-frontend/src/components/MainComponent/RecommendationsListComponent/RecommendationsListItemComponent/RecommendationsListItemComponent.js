import React from 'react';
import PropTypes from 'prop-types';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';
import RecommendationsListItemTextComponent from './RecommendationsListItemTextComponent/RecommendationsListItemTextComponent';
import { Link, Typography } from '@material-ui/core';
import Box from '@material-ui/core/Box';

const RecommendationsListItemComponent = ({ recommendation }) => (
  <React.Fragment>
    <ListItemAvatar>
      <Avatar
        src={recommendation.imagePath}
        style={{ height: '120px', width: '120px', marginRight: '0.8em' }} />
    </ListItemAvatar>
    <ListItemText
      primary={
        <Link href={recommendation.url}>
          <Typography variant="h6">{recommendation.title}</Typography>
        </Link>}
      secondary={
        <Box display="flex" justifyContent="space-between" flexDirection="row">
          <div>
            <RecommendationsListItemTextComponent
              label="Artist"
              text={recommendation.artist}
            >
            </RecommendationsListItemTextComponent>
            <RecommendationsListItemTextComponent
              label="Location"
              text={recommendation.location.name}
            >
            </RecommendationsListItemTextComponent>
            <RecommendationsListItemTextComponent
              label="Release date"
              text={recommendation.releaseDate}
            >
            </RecommendationsListItemTextComponent>
            <RecommendationsListItemTextComponent
              label="Tags"
              text={recommendation.tags.map(tag=> tag.split('/').pop()).join(', ')}
            >
            </RecommendationsListItemTextComponent>
          </div>
          <RecommendationsListItemTextComponent
            label="Matches"
            text={recommendation.matches}
            tooltipText="Ordering score based on matched tags"
          ></RecommendationsListItemTextComponent>
        </Box>
      }
    />
  </React.Fragment>

);

RecommendationsListItemComponent.propTypes = {
  recommendation: PropTypes.object
};

RecommendationsListItemComponent.defaultProps = {};

export default RecommendationsListItemComponent;
