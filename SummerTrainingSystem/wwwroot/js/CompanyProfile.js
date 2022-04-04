function checkform() {
    var f = document.forms["checkit"].elements;
    var cansubmit = true;
    if (f[1].value.length == 0) cansubmit = false;
    if (!cansubmit) {
        document.getElementById('SubmitComment').setAttribute('disabled', 'disabled');
        document.getElementById('SubmitComment').style.backgroundColor = '#d1d7dc';
        document.getElementById('SubmitComment').style.color = 'rgba(255, 255, 255, 0.8)';
    } else {
        document.getElementById('SubmitComment').removeAttribute('disabled');
        document.getElementById('SubmitComment').style.backgroundColor = '#0055D9';
        document.getElementById('SubmitComment').style.color = '#fff';
    }
}
function handleSubmit(e) {
    e.preventDefault();
    const CommentValue = document.getElementById("Message").value;
    const CompanyIdValue = document.getElementById("CompanyId").value;
    const commet = document.getElementById("comments-added");
    const noCommet = document.getElementById("NoComments");
    const Parent = document.getElementById("Parent");
    const data = {
        comment: CommentValue,
        hrCompanyId: CompanyIdValue
    }
    $.ajax({
        method: "post",
        url: '/comment/add',
        data: JSON.stringify(data),
        contentType: "application/json",
        beforeSend: (xhr) => {
            document.getElementById("spinner").classList.toggle('d-none');
        },
        success: (result, status, xhr) => {
            fullname = result.firstname + " " + result.lastname;
            ProfilePicUrl = result.profilePicUrl;
            document.getElementById("Message").value = "";
            var ParentDiv = document.createElement("div");
            ParentDiv.className = 'commentdiv';
            var imgChildDiv = document.createElement("div");
            imgChildDiv.className = 'photocommentdiv';
            var userImg = document.createElement("img");
            ProfilePicUrl = ProfilePicUrl == null ? '/images/avatar.svg' : '/uploads/' + ProfilePicUrl;
            userImg.src = ProfilePicUrl;
            imgChildDiv.appendChild(userImg);
            var commentChildDiv = document.createElement("div");
            commentChildDiv.className = 'commenttextdiv';
            var commenttext = document.createElement("div");
            var commentperson = document.createElement("div");
            commenttext.className = 'commenttext';
            commentperson.className = 'commentperson';
            document.getElementById('SubmitComment').setAttribute('disabled', 'disabled');
            document.getElementById('SubmitComment').style.backgroundColor = '#d1d7dc';
            document.getElementById('SubmitComment').style.color = 'rgba(255, 255, 255, 0.8)';
            var txt = document.createElement("div");
            txt.className = 'txt';
            commenttext.appendChild(txt);
            commentChildDiv.appendChild(commenttext);
            commentChildDiv.appendChild(commentperson);
            ParentDiv.appendChild(imgChildDiv);
            ParentDiv.appendChild(commentChildDiv);
            var h5 = document.createElement("h5");
            var a = document.createElement("a");
            txt.appendChild(h5);
            var textname = document.createTextNode(fullname);
            var CreateAt = document.createTextNode("Just Now");
            var spanTime = document.createElement("span");
            spanTime.className = "time";
            spanTime.appendChild(CreateAt);
            var br = document.createElement("br");
            var textcomment = document.createTextNode(CommentValue);
            a.appendChild(textname);
            h5.appendChild(a);
            commentperson.appendChild(spanTime);
            commentperson.appendChild(br);
            commentperson.appendChild(textcomment);
            commet?.appendChild(ParentDiv);
            if (Parent?.childElementCount > 0) {
                noCommet.className = 'd-none';
            }
            const spinner = document.getElementById("spinner");
            spinner.classList.toggle('d-none');
            toastr.options.closeButton = true;
            toastr.success('Review added successfully.');
        }
    });
}