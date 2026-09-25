let categories = [];
let currentEditIndex = null;

// Function to update the table
function updateTable() {
    const tableBody = document.getElementById('ProductTableBody');
    tableBody.innerHTML = ''; // Clear the table before updating
    if (categories.length === 0) {
        tableBody.innerHTML = '<tr><td colspan="10" class="text-center">No categories yet</td></tr>';
    } else {
        categories.forEach((Product, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
      <td>${index + 1}</td>
      <td>${Product.name}</td>
      <td>${Product.description}</td>
      <td>${Product.price}</td>
      <td>${Product.stock}</td>
      <td>${Product.rate}</td>
      <td>${Product.category}</td>
      <td>${Product.subCategory}</td>
      <td><img class="Product-image" src="${Product.image}" alt="${Product.name}"></td>
      <td>
        <button class="btn btn-info btn-sm" onclick="viewProduct(${index})"><i class="fas fa-eye"></i></button>
        <a href="edit-Product.html?id=${Product.id}" class="btn btn-warning btn-sm"><i class="fas fa-edit"></i></a>
        <button class="btn btn-danger btn-sm" onclick="deleteProduct(${index})"><i class="fas fa-trash"></i></button>
      </td>
    `;
            tableBody.appendChild(row);
        });
    }
}

// Function to add a Product
function addProduct(name, description, price, stock, rate, category, subCategory, image) {
    const newProduct = {
        id: Date.now(),
        name,
        description,
        price,
        stock,
        rate,
        category,
        subCategory,
        image
    };
    categories.push(newProduct);
    updateTable();
}

// Handler for the Add New Product form submit
function addProductHandler(event) {
    event.preventDefault();
    const name = document.getElementById('ProductName').value;
    const description = document.getElementById('ProductDescription').value;
    const price = document.getElementById('ProductPrice').value;
    const stock = document.getElementById('ProductStock').value;
    const rate = document.getElementById('ProductRate').value;
    const category = document.getElementById('ProductCategory').value;
    const subCategory = document.getElementById('ProductSubCategory').value;
    const imageInput = document.getElementById('ProductImage');
    const imageFile = imageInput.files[0];
    let imageUrl = '';

    if (imageFile) {
        const reader = new FileReader();
        reader.onloadend = function () {
            imageUrl = reader.result;
            addProduct(name, description, price, stock, rate, category, subCategory, imageUrl);
        };
        reader.readAsDataURL(imageFile);
    } else {
        addProduct(name, description, price, stock, rate, category, subCategory, imageUrl);
    }

    // Clear the input fields and close the modal
    document.getElementById('ProductName').value = '';
    document.getElementById('ProductDescription').value = '';
    document.getElementById('ProductPrice').value = '';
    document.getElementById('ProductStock').value = '';
    document.getElementById('ProductRate').value = '';
    document.getElementById('ProductCategory').value = '';
    document.getElementById('ProductSubCategory').value = '';
    document.getElementById('ProductImage').value = '';
    var myModal = bootstrap.Modal.getInstance(document.getElementById('addModal'));
    myModal.hide();
}

// Initialize the form submission event
document.getElementById('ProductForm').onsubmit = addProductHandler;

// Function to delete a Product
function deleteProduct(index) {
    categories.splice(index, 1);
    updateTable();
}

// Function to view a Product's details
function viewProduct(index) {
    const Product = categories[index];
    alert(`Viewing details for: ${Product.name}`);
}

// Initial call to display the categories
updateTable();