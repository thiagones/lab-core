import React from 'react';
import Header from './components/header'
import Main from './pages/main'

import 'bootstrap/dist/css/bootstrap.css';
import './styles.css'

class App extends React.Component {
  render() {
    return (
      <div className="app">
        <Header/>
        <Main/>
      </div>
    );
  }
}

export default App;
