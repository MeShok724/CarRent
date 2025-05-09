import { useEffect, useState } from 'react'
import '../styles/Main.css'
import { Link } from 'react-router-dom'
import type { Car } from '../interfaces/car'

function Main() {
    const [cars, setCars] = useState<Car[]>([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        fetch('https://localhost:7071/api/Cars')
            .then(response => response.json())
            .then(data => {
                setCars(data)
                setLoading(false)
            })
            .catch(error => {
                console.error('Ошибка при загрузке машин:', error)
                setLoading(false)
            })
    }, [])
    if (loading) return <p>Загрузка...</p>
  return (
      <div className="p-4">
          {cars.map(car => (
              <div className="card mb-4 shadow-sm" key={car.id} style={{ border: '1px solid #ccc', marginBottom: '1rem', padding: '1rem', display: 'flex', flexDirection: 'row' }}>
                  {car.carImages?.[0] && (
                      <img
                          src={car.carImages[0].imageUrl}
                          alt={`${car.brand} ${car.model}`}
                          style={{ width: '30%', maxHeight: '400px', objectFit: 'cover', borderRadius: '8px', marginBottom: '1rem', marginRight: 40 }}
                      />
                  )}
                  <div>
                      <h5 className="card-title pb-3">
                          <Link to={`/cars/${car.id}`} style={{ textDecoration: 'none'/*, color:'black'*/ }}>
                              {car.brand} {car.model} ({car.year})
                          </Link>
                      </h5>
                      <p>{car.category.name}, Цвет: {car.color}</p>
                      <p>Объем двигателя: {car.engineVolume} л, Пробег: {car.mileage} км</p>
                      <p>Цена аренды: {car.rentalPricePerDay} BYN / день</p>
                      <div style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
                          {car.carImages?.map(img => (
                              <img
                                  key={img.id}
                                  src={img.imageUrl}
                                  alt={`${car.brand} ${car.model}`}
                                  style={{ width: '150px', height: '100px', objectFit: 'cover', borderRadius: '4px' }}
                              />
                          ))}
                      </div>
                      <Link to={`/cars/${car.id}`} className="btn btn-primary" style={{marginTop: 30}}>
                          Подробнее
                      </Link>
                  </div>
              </div>
          ))}
      </div>
  );
}

export default Main;