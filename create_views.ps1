# Script to create all remaining views

# Activity Views
$activityList = @'
@model IEnumerable<Example.Service.Models.DTOs.ActivityFormResponseDto>
@{ViewData["Title"]="لیست فعالیتها";}
<div class="card"><div class="card-header bg-primary text-white d-flex justify-content-between align-items-center"><h4 class="mb-0"><i class="fas fa-tasks"></i> لیست فعالیتها</h4><a asp-action="Create" class="btn btn-light btn-sm"><i class="fas fa-plus"></i> ثبت جدید</a></div><div class="card-body"><table class="table table-striped"><thead class="table-dark"><tr><th>تاریخ</th><th>برنامه</th><th>شرکت کننده</th><th>عملیات</th></tr></thead><tbody>@if(Model!=null&&Model.Any()){@foreach(var item in Model){<tr><td>@item.ActivityDate.ToString("yyyy/MM/dd")</td><td>@item.SelectedProgramName</td><td>@string.Join(", ",item.StudentNames)</td><td><button class="btn btn-sm btn-outline-info" onclick="showDetails(@item.RowId)"><i class="fas fa-eye"></i></button><button class="btn btn-sm btn-outline-primary" onclick="editItem(@item.RowId)"><i class="fas fa-edit"></i></button><button class="btn btn-sm btn-outline-danger" onclick="deleteItem(@item.RowId)"><i class="fas fa-trash"></i></button></td></tr>}}else{<tr><td colspan="4" class="text-center">هیچ فعالیتی یافت نشد</td></tr>}</tbody></table></div></div>
<div class="modal fade" id="detailsModal" tabindex="-1"><div class="modal-dialog modal-lg"><div class="modal-content"><div class="modal-header bg-info text-white"><h5 class="modal-title">جزئیات</h5><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div><div class="modal-body" id="detailsModalBody"></div></div></div></div>
<div class="modal fade" id="editModal" tabindex="-1"><div class="modal-dialog modal-lg"><div class="modal-content"><div class="modal-header bg-warning text-dark"><h5 class="modal-title">ویرایش</h5><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div><div class="modal-body" id="editModalBody"></div></div></div></div>
<div class="modal fade" id="deleteModal" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header bg-danger text-white"><h5 class="modal-title">تایید حذف</h5><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div><div class="modal-body"><p>آیا از حذف اطمینان دارید؟</p></div><div class="modal-footer"><button type="button" class="btn btn-secondary" data-bs-dismiss="modal">انصراف</button><button type="button" class="btn btn-danger" id="confirmDeleteBtn">حذف</button></div></div></div></div>
@section Scripts{<script>let deleteId=0;function showDetails(id){$.get('/Activity/Details/'+id,function(data){$('#detailsModalBody').html(data);$('#detailsModal').modal('show');});}function editItem(id){$.get('/Activity/Edit/'+id,function(data){$('#editModalBody').html(data);$('#editModal').modal('show');});}function deleteItem(id){deleteId=id;$('#deleteModal').modal('show');}$('#confirmDeleteBtn').click(function(){$.post('/Activity/Delete',{id:deleteId},function(result){if(result.success){$('#deleteModal').modal('hide');location.reload();}else{alert('خطا: '+result.message);}});});$(document).on('submit','#editForm',function(e){e.preventDefault();$.post($(this).attr('action'),$(this).serialize(),function(result){if(result.success){$('#editModal').modal('hide');location.reload();}else{alert('خطا: '+result.message);}});});</script>}
'@

$activityDetails = @'
@model Example.Service.Models.DTOs.ActivityFormResponseDto
<div class="row"><div class="col-md-6 mb-3"><strong>تاریخ فعالیت:</strong><p>@Model.ActivityDate.ToString("yyyy/MM/dd")</p></div><div class="col-md-6 mb-3"><strong>برنامه:</strong><p>@Model.SelectedProgramName</p></div><div class="col-md-12 mb-3"><strong>شرکت کنندگان:</strong><p>@string.Join(", ",Model.StudentNames)</p></div></div>
'@

$activityEdit = @'
@model Example.Service.Models.DTOs.ActivityFormUpdateModel
<form id="editForm" asp-action="Edit" method="post"><input type="hidden" asp-for="RowId" /><div class="mb-3"><label asp-for="ActivityDate" class="form-label">تاریخ فعالیت</label><input asp-for="ActivityDate" class="form-control" type="date" /></div><div class="mb-3"><label asp-for="SelectedProgramId" class="form-label">برنامه</label><input asp-for="SelectedProgramId" class="form-control" type="number" /></div><button type="submit" class="btn btn-primary"><i class="fas fa-save"></i> ذخیره</button></form>
'@

# Write Activity files
Set-Content -Path "Sediq.Web.Api\Views\Activity\List.cshtml" -Value $activityList -Encoding UTF8
Set-Content -Path "Sediq.Web.Api\Views\Activity\_DetailsPartial.cshtml" -Value $activityDetails -Encoding UTF8
Set-Content -Path "Sediq.Web.Api\Views\Activity\_EditPartial.cshtml" -Value $activityEdit -Encoding UTF8

Write-Host "Activity views created successfully!"

# Similar pattern for DurationDate, ScoreForm, Sedig...
Write-Host "Run this script from project root directory to create all views"
