import React from 'react';
import './NoSearchResult.css';
const NoSearchResult=()=> {
  return (
    
    <div >
        <img className='noSearchResult' height='150' width='150' src={require('./nosearchresult.jpg')} />
   <h2 className='NIh2'>No Search Result found!</h2>
  </div>
  

  );

}

export default NoSearchResult;
