import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Container from '@material-ui/core/Container';
import SearchComponent from './SearchComponent/SearchComponent'
import RecommendationsListComponent from './RecommendationsListComponent/RecommendationsListComponent'
import { getRecommendations } from '../../services/RecommendationsService';
import { CircularProgress } from '@material-ui/core';


const MainComponent = () => {
  const [recommendations, setRecommendations] = useState([]);
  const [loading, setLoading] = useState(false);

  const onRecommendationsTriggered = (description) => {
    setLoading(true);
    getRecommendations(description)
      .then(newRecommendations =>  {
        setLoading(false);
        setRecommendations(newRecommendations);
      });
  };
  
  return (
    <Container maxWidth='xl'>
      <SearchComponent recommendationsTriggered={onRecommendationsTriggered}></SearchComponent>
      <div style={{marginTop: '1.5em', textAlign: 'center'}}>
        { loading ? 
          <CircularProgress></CircularProgress> :
          <RecommendationsListComponent recommendations={recommendations} ></RecommendationsListComponent>
        }
      </div>
    </Container>
  );
};

MainComponent.propTypes = {};

MainComponent.defaultProps = {};

export default MainComponent;
