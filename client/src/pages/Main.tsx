import { useEffect, useState } from 'react'
import '../styles/Main.css'

interface CarImage {
    id: number
    carId: number
    imageUrl: string
}
interface Car {
    id: number
    brand: string
    model: string
    year: number
    licensePlate: string
    color: string
    mileage: number
    rentalPricePerDay: number
    carImages: CarImage[]
}

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
      <div style={{paddingLeft: 30}}>
          <h2>Список автомобилей</h2>
          {cars.map(car => (
              <div key={car.id} style={{ border: '1px solid #ccc', marginBottom: '1rem', padding: '1rem', display: 'flex' }}>
                  {car.carImages?.[0] && (
                      <img
                          src={car.carImages[0].imageUrl}
                          alt={`${car.brand} ${car.model}`}
                          style={{ width: '30%', maxHeight: '400px', objectFit: 'cover', borderRadius: '8px', marginBottom: '1rem', marginRight: 40 }}
                      />
                  )}
                  <div>
                      <h3>{car.brand} {car.model} ({car.year})</h3>
                      <p>Цвет: {car.color}, Пробег: {car.mileage} км</p>
                      <p>Цена аренды: {car.rentalPricePerDay} BYN / день</p>
                      <p>Номер: {car.licensePlate}</p>
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
                  </div>
              </div>
          ))}
      </div>
  );
}

export default Main;