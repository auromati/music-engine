import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Container from '@material-ui/core/Container';
import SearchComponent from './SearchComponent/SearchComponent'
import RecommendationsListComponent from './RecommendationsListComponent/RecommendationsListComponent'
import { getRecommendations } from '../../services/RecommendationsService';
import { CircularProgress } from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';

const MainComponent = () => {
  const [recommendations, setRecommendations] = useState([]);
  const [loading, setLoading] = useState(false);
  const [currentDescription, setCurrentDescription] = useState('');
  const [currentPage, setCurrentPage] = useState(0);

  const callGetRecommendations = (description, page) => {
    setLoading(true);
    getRecommendations(description, page)
      .then(newRecommendations =>  {
        setLoading(false);
        setRecommendations(newRecommendations);
      });
  };

  const onRecommendationsTriggered = (description) => {
    setCurrentDescription(description);
    callGetRecommendations(description, currentPage);
  };

  const handleChangePage = (event, newPage) => {
    setCurrentPage(newPage);
    callGetRecommendations(currentDescription, newPage);
  };
  
  return (
    <Container maxWidth='xl'>
      <SearchComponent recommendationsTriggered={onRecommendationsTriggered}></SearchComponent>
      <div style={{margin: '1.5em 0', textAlign: 'center'}}>
        { loading ? 
          <CircularProgress></CircularProgress> :
          <RecommendationsListComponent recommendations={recommendations} ></RecommendationsListComponent>
        }
      </div>
      <Pagination count={10} onChange={handleChangePage}/>
    </Container>
  );
};

MainComponent.propTypes = {};

MainComponent.defaultProps = {};

export default MainComponent;
