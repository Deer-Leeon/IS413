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
        <div>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Team</th>
                        <th>Address</th>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    {bowlers.map((bowler) => (
                        <tr key={bowler.bowlerID}>
                            <td>{bowler.name}</td>
                            <td>{bowler.teamName}</td>
                            <td>{bowler.address}</td>
                            <td>{bowler.city}</td>
                            <td>{bowler.state}</td>
                            <td>{bowler.zip}</td>
                            <td>{bowler.phone}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default BowlersTable;