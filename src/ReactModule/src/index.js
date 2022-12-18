import React from 'react';
import ReactDOM from 'react-dom/client';
import { HashRouter as ReactRouter } from 'react-router-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { RoutesPage } from './RoutesPage'

export var data = [
  
]

window.loadImages = (imgsource) => {
   if(imgsource != undefined){
      data = imgsource.split(";");
   }
   /*
   data = [
    "https://live.staticflickr.com/65535/52563028656_b4989029eb.jpg",
  "https://live.staticflickr.com/65535/52563307389_a6f755901b.jpg",
  "https://live.staticflickr.com/65535/52563483790_52be1246a3.jpg",
  "https://live.staticflickr.com/65535/52563543053_71d7e445b2.jpg",
  "https://live.staticflickr.com/65535/52563004531_df9de58b45.jpg",
  "https://live.staticflickr.com/65535/52562502797_05c220b61d.jpg",
  "https://live.staticflickr.com/65535/52562518877_1a7fd2b3ef.jpg",
  "https://live.staticflickr.com/65535/52562500472_94f6197f53.jpg",
  "https://live.staticflickr.com/65535/52563419140_94bac7544a.jpg",
  "https://live.staticflickr.com/65535/52562500127_b157ab122c.jpg",
  "https://live.staticflickr.com/65535/52562954901_48def054a8.jpg",
  "https://live.staticflickr.com/65535/52563238364_77132ef7fa.jpg",
  "https://live.staticflickr.com/65535/52563491883_5c0107ffee.jpg",
  "https://live.staticflickr.com/65535/52563491748_7ee1251a0d.jpg",
  "https://live.staticflickr.com/65535/52563237704_2efe8a51d7.jpg",
  "https://live.staticflickr.com/65535/52563237269_2dd2600664.jpg",
  "https://live.staticflickr.com/65535/52562498182_c38f316972.jpg",
  "https://live.staticflickr.com/65535/52563490638_669efdcf58.jpg",
  "https://live.staticflickr.com/65535/52563416545_9388780ea7.jpg",
  "https://live.staticflickr.com/65535/52563236139_aa2a2ae0b8.jpg",
  "https://live.staticflickr.com/65535/52562497147_65cf5f4e01.jpg",
  "https://live.staticflickr.com/65535/52562951856_afa555709a.jpg",
  "https://live.staticflickr.com/65535/52563489048_8d6a11da7f.jpg",
  "https://live.staticflickr.com/65535/52563235249_09b80fccf5.jpg",
  "https://live.staticflickr.com/65535/52563234979_299cd2f7e3.jpg",
  "https://live.staticflickr.com/0/52563229404_c8e7c7d265.jpg",
  "https://live.staticflickr.com/65535/52563414870_ea9e4e32ba.jpg",
  "https://live.staticflickr.com/65535/52563234669_5880aaf5c7.jpg",
  "https://live.staticflickr.com/65535/52562495317_ac1ec1d7a1.jpg",
  "https://live.staticflickr.com/65535/52562950146_3e7709951e.jpg",
  "https://live.staticflickr.com/65535/52562949991_abf19041b5.jpg",
  "https://live.staticflickr.com/65535/52562494832_7655de0a67.jpg",
  "https://live.staticflickr.com/65535/52563233479_b941c63ac6.jpg",
  "https://live.staticflickr.com/65535/52563487183_c28b605df2.jpg",
  "https://live.staticflickr.com/65535/52562949406_8909642ddf.jpg",
  "https://live.staticflickr.com/65535/52563413260_e05ab11a79.jpg",
  "https://live.staticflickr.com/65535/52563232689_434c733b0e.jpg",
  "https://live.staticflickr.com/65535/52563412670_4e405903a7.jpg",
  "https://live.staticflickr.com/65535/52563485913_8d817e89c2.jpg",
  "https://live.staticflickr.com/65535/52562948046_b45e83fb8b.jpg",
  "https://live.staticflickr.com/65535/52563411900_883fd6956a.jpg",
  "https://live.staticflickr.com/65535/52562947571_858104427d.jpg",
  "https://live.staticflickr.com/65535/52562947391_f2f5a51ce5.jpg",
  "https://live.staticflickr.com/65535/52563231169_a8ec02f631.jpg",
  "https://live.staticflickr.com/65535/52562947101_736006fe44.jpg",
  "https://live.staticflickr.com/65535/52563410780_a6cb74fb15.jpg",
  "https://live.staticflickr.com/65535/52563230584_328df5205e.jpg",
  "https://live.staticflickr.com/65535/52563483993_c0ab4490d2.jpg",
  "https://live.staticflickr.com/65535/52562491017_deceb9310f.jpg",
  "https://live.staticflickr.com/65535/52563483793_dd6a7e6354.jpg",
  "https://live.staticflickr.com/65535/52563410120_5b8434afb5.jpg",
  "https://live.staticflickr.com/65535/52563483578_197cf4fef8.jpg",
  "https://live.staticflickr.com/65535/52563229764_1c16f14ac9.jpg",
  "https://live.staticflickr.com/65535/52563483333_7f4548771d.jpg",
  "https://live.staticflickr.com/65535/52563229089_75ae43e0bc.jpg",
  "https://live.staticflickr.com/65535/52563228749_dfa1fff851.jpg",
  "https://live.staticflickr.com/65535/52563408540_9d8d571872.jpg",
  "https://live.staticflickr.com/65535/52563408350_30b2eb42d1.jpg",
  "https://live.staticflickr.com/65535/52562943946_3e96d2ca2c.jpg",
  "https://live.staticflickr.com/65535/52563408005_ff440e0539.jpg",
  "https://live.staticflickr.com/65535/52562488212_6ca9c6d310.jpg",
  "https://live.staticflickr.com/65535/52562487827_ab845f810c.jpg",
  "https://live.staticflickr.com/65535/52562487682_43c782dc69.jpg",
  "https://live.staticflickr.com/65535/52562487512_e19f2c4e56.jpg",
  "https://live.staticflickr.com/65535/52563480358_9c8bab8508.jpg",
  "https://live.staticflickr.com/65535/52562486987_32daf5ed88.jpg",
  "https://live.staticflickr.com/65535/52563479678_898af0c99d.jpg",
  "https://live.staticflickr.com/65535/52563225759_0ae3df2aa2.jpg",
  "https://live.staticflickr.com/65535/52562941276_b86cceee63.jpg",
  "https://live.staticflickr.com/65535/52563479093_32cc21bfa4.jpg",
  "https://live.staticflickr.com/65535/52563405500_5270559c9c.jpg",
  "https://live.staticflickr.com/65535/52563405295_898b66abf5.jpg",
  "https://live.staticflickr.com/65535/52563224759_4042e66580.jpg",
  "https://live.staticflickr.com/65535/52562940206_0fb74a361e.jpg",
  "https://live.staticflickr.com/65535/52563478123_6c5dfd0870.jpg",
  "https://live.staticflickr.com/65535/52562939826_8379021385.jpg",
  "https://live.staticflickr.com/65535/52563404230_ef89446808.jpg",
  "https://live.staticflickr.com/65535/52563477513_b792b4e0b0.jpg",
  "https://live.staticflickr.com/65535/52563223674_2cfdb7cd80.jpg",
  "https://live.staticflickr.com/65535/52563476983_18f4ebccc4.jpg",
  "https://live.staticflickr.com/65535/52563222959_39ecf67517.jpg",
  "https://live.staticflickr.com/65535/52562938156_9db5d8e82d.jpg",
  "https://live.staticflickr.com/65535/52563401955_66e3f3d249.jpg",
  "https://live.staticflickr.com/65535/52562482242_895f023d53.jpg",
  "https://live.staticflickr.com/65535/52563475218_51a0729073.jpg",
  "https://live.staticflickr.com/65535/52562481967_18e07686ca.jpg",
  "https://live.staticflickr.com/65535/52563474853_cebcff49c9.jpg",
  "https://live.staticflickr.com/65535/52562936506_c8a511207a.jpg",
  "https://live.staticflickr.com/65535/52563474423_0816f154f1.jpg",
  "https://live.staticflickr.com/65535/52563400590_8de22cd9d4.jpg",
  "https://live.staticflickr.com/65535/52563400405_c2e998bd60.jpg",
  "https://live.staticflickr.com/65535/52562935621_af9c62872a.jpg",
  "https://live.staticflickr.com/65535/52563473793_f4c1e4202c.jpg",
  "https://live.staticflickr.com/65535/52563399920_71886aa4de.jpg",
  "https://live.staticflickr.com/65535/52563472823_31813b759a.jpg",
  "https://live.staticflickr.com/65535/52562934131_63233d6be8.jpg",
  "https://live.staticflickr.com/65535/52563472128_1c0409e6d1.jpg",
  "https://live.staticflickr.com/65535/52562933616_7282382851.jpg",
  "https://live.staticflickr.com/65535/52563471843_25bb588ee0.jpg",
  "https://live.staticflickr.com/65535/52562933351_e25a8eb073.jpg"
]
*/
  window.location.hash = '#/displayImages';

}

window.reLoadGallery =  () => {
  window.loadingPage();
  setTimeout( ()=>{
      window.location.hash = '#/displayImages'
      },
      100
  )}

window.loadHomePage =  () => {
  console.log("In Home page");
  window.location.hash = '#/homePage';
}

window.loadingPage=()=>{
  console.log("In Loadingapge");
  window.location.hash='#/loadingPage';
}

window.noInternetPage=()=>{
  console.log("In noInternetPage");
  window.location.hash='#/noInternet';
}

window.noSearchResultPage=()=>{
  console.log("In No search result page");
  window.location.hash='#/noSearchResult';
}


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <ReactRouter>
    <RoutesPage />
  </ReactRouter>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
