import React from 'react';
import './NoInternet.css';
const NoInternet=()=> {
  return (
    
    <div >
        <img className='noInternet' height='150' width='150' src={require('./noInternet.png')} />
   <h2 className='NIh2'>No Internet!</h2>
   <h3 className='NIh3'>Please check your internet connection!</h3>
  </div>
  

  );

}

export default NoInternet;
