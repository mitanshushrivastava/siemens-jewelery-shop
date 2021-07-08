import React from 'react';
import './App.css';
import { LoginPage } from './features/loginPage/LoginPage';

function App() {
  return (
    <div className="App">
      <header className="App-header">
      <h2>Welcome to Jewelery Shop</h2>
        <LoginPage />
      </header>
    </div>
  );
}

export default App;
