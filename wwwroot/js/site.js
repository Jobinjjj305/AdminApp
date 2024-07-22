// Example using JavaScript fetch API
fetch('/Invoice/GeneratePdf', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        InvoiceNumber: 1001,
        InvoiceDate: '2024-07-19',
        CustomerName: 'John Doe',
        Amount: 500.00
    })
})
    .then(response => {
        // Handle response
    })
    .catch(error => {
        console.error('Error:', error);
    });
