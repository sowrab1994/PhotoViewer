import React, { useState, useEffect } from 'react';
import './gallery.css';
import { data } from './index';
const config = {
    rootMargin: "0px 0px 0px 0px",
    threshold: 0.2,
  };
const Gallery = () => {
    const [modal, setModal] = useState(false);
    const [showPicture, setShowPicture] = useState(false);
    const [openPictureSrc, setOpenPictureSrc] = useState(null);
    const [height, setHeight] = useState(null);
    const [width, setWidth] = useState(null);

    const handleClickEvent = (src) => {
        const img = new Image();
        img.src = src;
        console.log("testing image src ____ ", img.src);
        img.onload = () => {
            setHeight(img.height);
            setWidth(img.width);
        };
        img.onerror = (err) => {
            console.log("img error");
            console.error(err);
        };
        setShowPicture(true);
        setOpenPictureSrc(src);
        setModal(true);
    };

    const handleZoomIn = () => {
        setHeight((prevHeight) => prevHeight + 100);
        setWidth((prevWidht) => prevWidht + 100);
    };

    const handleZoomOut = () => {
        if (height <= 200 || width <= 200) {
            return;
        } else {
            setHeight((prevHeight) => prevHeight - 100);
            setWidth((prevWidht) => prevWidht - 100);
        }
    };
    const sleep = ms => new Promise(
        resolve => setTimeout(resolve, ms)
      );

    const closePicture = () => {
        setShowPicture(false);
        setHeight(null);
        setWidth(null);
        setModal(false);
        window.reLoadGallery();
    };


    useEffect(() => {
        let observer = new window.IntersectionObserver(function (entries, self) {
          entries.forEach((entry) => {
            if (entry.isIntersecting) { 
              loadImages(entry.target);
              self.unobserve(entry.target);
            }
          });
        }, config);
    
        const imgs = document.querySelectorAll("[data-src]");
        imgs.forEach((img) => {
          observer.observe(img);
        });
        return () => {
          imgs.forEach((img) => {
            observer.unobserve(img);
          });
        };
      }, []);
    
      const loadImages = (image) => {
        image.src = image.dataset.src;
      };
    

    return (
        <>
            {showPicture ? (
                <div className={modal ? "modal open" : "modal"}>
                    <button className='zoomIn' color="white" onClick={() => handleZoomIn()}>+</button>
                    <button className='zoomOut' color="white" onClick={() => handleZoomOut()}>- </button>
                    <img className='pics' src={openPictureSrc} style={{ height: Number(`${height}`), width: Number(`${width}`) }} alt="Not found"/>
                    <button color="red" className='closeButton' onClick={() => closePicture()}>X</button>
                </div>

            ) : (

                <div>
                    <h2>These are your search results</h2>
                    <div className='gallery' style={{}}>
                        {data.map((item, index) => {
                            return (
                                <div className='pics' style={{ width: '19.5%', height: '19.5%', }} key={index} >
                                    <img src={""} data-src={item} style={{ width: '100%', height: '100%', }} onClick={() => handleClickEvent(item)} alt="Not found" />
                                </div>
                            );
                        })}
                    </div>
                </div>
            )}
        </>
    );
}

export default Gallery;