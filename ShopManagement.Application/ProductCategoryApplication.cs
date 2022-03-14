using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجددا تلاش کنید");

            var slug = command.Slug.Slugify();

            //var picturePath = $"{command.Slug}";
            //var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            var productCategory = new ProductCategory(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();


        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد. لطفا مجدد تلاش کنید");

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد. لطفا مجدد تلاش کنید");

            var slug = command.Slug.Slugify();

            //var picturePath = $"{command.Slug}";
            //var fileName = _fileUploader.Upload(command.Picture, picturePath);

            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
