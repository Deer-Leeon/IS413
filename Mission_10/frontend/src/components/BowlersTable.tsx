import React, { useEffect, useState } from 'react';
import axios from 'axios';

// Define the structure of a Bowler object
interface Bowler {
    bowlerID: number; // Match API response (camelCase)
    name: string; // Match API response (camelCase)
    teamName: string; // Match API response (camelCase)
    address: string; // Match API response (camelCase)
    city: string; // Match API response (camelCase)
    state: string; // Match API response (camelCase)
    zip: string; // Match API response (camelCase)
    phone: string; // Match API response (camelCase)
}

const BowlersTable = () => {
    // State to store the list of bowlers
    const [bowlers, setBowlers] = useState<Bowler[]>([]);
    // State to handle loading status
    const [loading, setLoading] = useState(true);
    // State to handle errors
    const [error, setError] = useState<string | null>(null);

    // Fetch bowlers data from the API
    useEffect(() => {
        const fetchBowlers = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/bowlers');
                console.log('API Response:', response.data); // Log the response
                setBowlers(response.data); // Set the fetched data to state
            } catch (error) {
                console.error('Error fetching bowlers:', error);
                setError(`Failed to fetch bowlers. Please try again later`); // Set error message
            } finally {
                setLoading(false); // Set loading to false after the request completes
            }
        };

        fetchBowlers();
    }, []); // Empty dependency array ensures this runs only once on mount

    // Display a loading message while data is being fetched
    if (loading) {
        return <div>Loading bowlers...</div>;
    }

    // Display an error message if the request fails
    if (error) {
        return <div>Error: {error}</div>;
    }

    // Render the table with bowler data
    return (
        <div style={{ margin: '20px', textAlign: 'center' }}>
            <table style={{ 
                width: '80%', 
                margin: '0 auto', 
                borderCollapse: 'collapse', 
                boxShadow: '0 0 10px rgba(0, 0, 0, 0.1)',
                border: '1px solid #ddd'
            }}>
                <thead>
                    <tr style={{ backgroundColor: '#082E5C', color: 'white' }}>
                        <th style={{ padding: '12px', textAlign: 'left' }}>Name</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>Team</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>Address</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>City</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>State</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>Zip</th>
                        <th style={{ padding: '12px', textAlign: 'left' }}>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    {bowlers.map((bowler) => (
                        <tr key={bowler.bowlerID} style={{ borderBottom: '1px solid #ddd' }}>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.name}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.teamName}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.address}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.city}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.state}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.zip}</td>
                            <td style={{ padding: '12px', textAlign: 'left' }}>{bowler.phone}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default BowlersTable;