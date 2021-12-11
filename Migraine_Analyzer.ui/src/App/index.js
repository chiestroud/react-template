import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import NavBar from '../components/NavBar';
import Routes from '../helpers/Routes';
import './App.scss';

function App() {
  return (
    <Router>
      <NavBar />
      <Routes />
    </Router>
  );
}

export default App;
