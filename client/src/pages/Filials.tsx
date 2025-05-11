import { useEffect, useState } from "react"
import type { Branch } from "../interfaces/branch"

function Filials() {
    const [branches, setBranches] = useState<Branch[]>([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        fetch('https://localhost:7071/api/Branches')
            .then(response => response.json())
            .then(data => {
                setBranches(data)
                setLoading(false)
            })
            .catch(error => {
                console.error('Ошибка при загрузке машин:', error)
                setLoading(false)
            })
    }, [])
    if (loading) return <p>Загрузка...</p>
    return (
        <div className="p-4 w-100 row justify-content-center">
            {branches.map(branch => (
                <div className="card mb-4 shadow-sm w-50" key={branch.id} style={{  border: '1px solid #ccc', marginBottom: '1rem', padding: '1rem', display: 'flex', flexDirection: 'row' }}>
                    <div>
                        <h5 className="card-title pb-3">{branch.name}</h5>
                        <p>Город: {branch.city}</p>
                        <p>Адрес: {branch.address}</p>
                        <p>Почтовый индекс: {branch.postalCode}</p>
                        <p>Телефон: {branch.phone}</p>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default Filials;