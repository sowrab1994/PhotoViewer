import React from "react";
import {Routes, Route, Navigate} from 'react-router-dom'
import NoInternet from "./NoInternet";
import Gallery from "./Gallery";
import HomePage from "./HomePage";
import Spinner from "./Spinner";
import NoSearchResult from "./NoSearchResult";
export const RoutesPage = () => {
    return (
        <Routes>
            <Route path="/homePage" element={<HomePage/>}/>
            <Route path="/noInternet" element={<NoInternet/>}/>
            <Route path="/noSearchResult" element={<NoSearchResult/>}/>
            <Route path="/displayImages" element={<Gallery/>}/>
            <Route path="/loadingPage" element={<Spinner/>}/>
            <Route path="/" element={<HomePage/>}/>
        </Routes>
    )
}