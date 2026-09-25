// document.getElementById('priceRange').addEventListener('input', function () {
//     document.getElementById('priceRangeLabel').textContent = this.value;
// });

// // Function to filter products by search, category, and price
// function filterProducts() {
//     const searchInput = document.getElementById('searchInput').value.toLowerCase();
//     const selectedCategory = document.getElementById('categoryFilter').value;
//     const maxPrice = parseFloat(document.getElementById('priceRange').value);
//     const productCards = document.querySelectorAll('.product-card');

//     productCards.forEach(card => {
//         const productName = card.querySelector('.product-name').textContent.toLowerCase();
//         const productCategory = card.getAttribute('data-category');
//         const productPrice = parseFloat(card.getAttribute('data-price'));

//         // Show or hide card based on filter conditions
//         if (
//             (productName.includes(searchInput)) &&
//             (selectedCategory === "" || productCategory === selectedCategory) 
//         ) {
//             card.style.display = 'block';  // Show the product
//         } else {
//             card.style.display = 'none';  // Hide the product
//         }
//     });
// }

// Function to filter products by search and category
function filterProducts() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const selectedCategory = document.getElementById('categoryFilter').value;
    const productCards = document.querySelectorAll('.product-card');

    productCards.forEach(card => {
        const productName = card.querySelector('.product-name').textContent.toLowerCase();
        const productCategory = card.getAttribute('data-category');

        // Show or hide card based on filter conditions
        if (
            (productName.includes(searchInput)) &&
            (selectedCategory === "" || productCategory === selectedCategory)
        ) {
            card.style.display = 'block';  // Show the product
        } else {
            card.style.display = 'none';  // Hide the product
        }
    });
}

// Trigger filterProducts on page load to show all products initially
document.addEventListener('DOMContentLoaded', filterProducts);

// Add event listeners to all elements
document.getElementById('searchInput').addEventListener('keyup', filterProducts);
document.getElementById('categoryFilter').addEventListener('change', filterProducts);

// Function to handle star rating clicks
function handleStarRating(event) {
    const stars = event.target.closest('.star-rating').querySelectorAll('.star');
    const ratingValue = event.target.getAttribute('data-value');
    const productId = event.target.closest('.star-rating').getAttribute('data-product-id');

    stars.forEach((star) => {
        if (parseInt(star.getAttribute('data-value')) <= parseInt(ratingValue)) {
            star.classList.add('selected');
        } else {
            star.classList.remove('selected');
        }
    });

    console.log(`Product ${productId} rated ${ratingValue} star(s)`);
}

// Add event listeners to all stars
document.querySelectorAll('.star').forEach(star => {
    star.addEventListener('click', handleStarRating);
});
