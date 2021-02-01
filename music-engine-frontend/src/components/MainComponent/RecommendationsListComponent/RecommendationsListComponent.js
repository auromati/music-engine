import React from 'react';
import PropTypes from 'prop-types';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import RecommendationsListItemComponent from './RecommendationsListItemComponent/RecommendationsListItemComponent';
import { Card, CardContent } from '@material-ui/core';

const RecommendationsListComponent = ({ recommendations }) => {
  return recommendations && recommendations.length ? (
    <List>
      {recommendations.map((recommendation, index) =>
      (<React.Fragment key={index}>
        <ListItem alignItems="flex-start">
          <RecommendationsListItemComponent recommendation={recommendation}></RecommendationsListItemComponent>
        </ListItem>
        {
          index !== recommendations.length - 1 ? <Divider></Divider> : null
        }
      </React.Fragment>))
      }
    </List>
  )
  :
  <Card variant="outlined" style={{textAlign: 'center'}}>
    <CardContent >
      No recommendations found
    </CardContent>
  </Card>
}

RecommendationsListComponent.propTypes = {
  recommendations: PropTypes.array
};

RecommendationsListComponent.defaultProps = {};

export default RecommendationsListComponent;
