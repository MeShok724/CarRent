import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'

interface CarImage {
    id: number
    carId: number
    imageUrl: string
}

interface Props {
    carImages: CarImage[]
}

const CarImageCarousel: React.FC<Props> = ({ carImages }) => {
    if (!carImages || carImages.length === 0) return null

    return (
        <div id="carouselExampleIndicators" className="carousel slide" data-bs-ride="carousel">
            <div className="carousel-indicators">
                {carImages.map((_, index) => (
                    <button
                        type="button"
                        data-bs-target="#carouselExampleIndicators"
                        data-bs-slide-to={index}
                        className={index === 0 ? 'active' : ''}
                        aria-current={index === 0}
                        aria-label={`Slide ${index + 1}`}
                        key={index}
                    />
                ))}
            </div>

            <div className="carousel-inner">
                {carImages.map((img, index) => (
                    <div
                        className={`carousel-item ${index === 0 ? 'active' : ''}`}
                        key={img.id}
                    >
                        <img
                            src={img.imageUrl}
                            className="d-block w-100 rounded"
                            alt={`Фото ${index + 1}`}
                            style={{ height:'600px', objectFit: 'cover' }}
                        />
                    </div>
                ))}
            </div>

            <button
                className="carousel-control-prev"
                type="button"
                data-bs-target="#carouselExampleIndicators"
                data-bs-slide="prev"
            >
                <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Previous</span>
            </button>
            <button
                className="carousel-control-next"
                type="button"
                data-bs-target="#carouselExampleIndicators"
                data-bs-slide="next"
            >
                <span className="carousel-control-next-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Next</span>
            </button>
        </div>
    )
}

export default CarImageCarousel
