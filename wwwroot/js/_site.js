// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var proto = location.protocol;
var host =  location.hostname;
var port =  location.port;
var url_arr = window.location.href.split('/');

if (url_arr[url_arr.length-1] == "Users"){   
    const uri = proto+'//'+host+(port ? ':'+location.port: '')+'/api/users';
    fetchUsers();
}
else{
    const uri = proto+'//'+host+(port ? ':'+location.port: '')+'/api/products';
    searchProducts();
}


function searchProducts() {
    let products = null;
    function getCount(data) {
      const el = $("#counter");
      let lender = "product";
      if (data) {
        if (data > 1) {
          lender = "products";
        }
        el.text(data + " " + lender);
      } else {
        el.text("No " + lender);
      }
    }

    $(document).ready(function() {
      getProducts();
    });

    function getProducts() {
      $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function(data) {
          const tBody = $("#products");

          $(tBody).empty();

          getCount(data.length);

          $.each(data, function(key, lender) {
            const tr = $("<tr></tr>")
              .append($("<td></td>").text(lender.lender))
              .append($("<td></td>").text(lender.interestRate))
              .append($("<td></td>").text(lender.rateType))
              .append($("<td></td>").text(lender.ltvRatio))

            tr.appendTo(tBody);
          });

          products = data;
        }
      });
    }

}

function fetchUsers(){
    let users = null;
    function getCount(data) {
      const el = $("#counter");
      let firstname = "user";
      if (data) {
        if (data > 1) {
          firstname = "users";
        }
        el.text(data + " " + firstname);
      } else {
        el.text("No " + firstname);
      }
    }

    $(document).ready(function() {
      getData();
    });

    function getData() {
      $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function(data) {
          const tBody = $("#users");

          $(tBody).empty();

          getCount(data.length);

          $.each(data, function(key, user) {
            const tr = $("<tr></tr>")
              .append($("<td></td>").text(user.firstName))
              .append($("<td></td>").text(user.lastName))
              .append($("<td></td>").text(user.emailAddress))
              .append($("<td></td>").text(user.dateOfBirth))
              .append(
                $("<td></td>").append(
                  $("<button>Edit</button>").on("click", function() {
                    editUser(user.id);
                  })
                )
              )
              .append(
                $("<td></td>").append(
                  $("<button>Delete</button>").on("click", function() {
                    deleteUser(user.id);
                  })
                )
              );

            tr.appendTo(tBody);
          });

          users = data;
        }
      });
    }
}

function addUser() {
  const user = {
    firstName: $("#add-firstname").val(),
    lastName: $("#add-lasttname").val(),
    emailAddress: $("#add-emailaddress").val(),
    dateOfBirth: $("#add-dateofbirth").val()
  };

  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: uri,
    contentType: "application/json",
    data: JSON.stringify(user),
    error: function(jqXHR, textStatus, errorThrown) {
      alert("Opps... Something went wrong!");
    },
    success: function(result) {
      getData();
      $("#add-user").val("");
    }
  });
}

function deleteUser(id) {
  $.ajax({
    url: uri + "/" + id,
    type: "DELETE",
    success: function(result) {
      getData();
    }
  });
}

function editUser(id) {
  $.each(users, function(key, user) {
    if (user.id === id) {
        $("#edit-id").val(user.id);
        $("#edit-firstname").val(user.firstName);
        $("#edit-lastname").val(user.lastName);
        $("#edit-emailaddress").val(user.emailAddress);  
        $("#edit-dateofbirth").val(user.dateOfBirth);
    }
  });
  $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function() {
  const user = {
    id: $("#edit-id").val(),
    firstName: $("#edit-firstname").val(),
    lastName: $("#edit-lastname").val(),
    emailAddress: $("#edit-emailaddress").val(),
    dateOfBirth: $("#edit-dateofbirth").val(),
  };

  $.ajax({
    url: uri + "/" + $("#edit-id").val(),
    type: "PUT",
    accepts: "application/json",
    contentType: "application/json",
    data: JSON.stringify(user),
    success: function(result) {
      getData();
    }
  });

  closeInput();
  return false;
});

function closeInput() {
  $("#spoiler").css({ display: "none" });
}