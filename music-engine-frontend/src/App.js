import './App.css';
import React from 'react';
import HeaderComponent from './components/HeaderComponent/HeaderComponent';
import MainComponent from './components/MainComponent/MainComponent';

function App() {
  return (
    <React.Fragment>
      <HeaderComponent></HeaderComponent>
      <MainComponent></MainComponent>
    </React.Fragment>
  );
}

export default App;
