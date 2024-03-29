//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(CallBook.MyModel),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets05d4af927f00e4d003d42058b5a420d4e33dae5f6caf1c7e3da0fae10a759678))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework Power Tools", "0.9.0.0")]
    internal sealed class ViewsForBaseEntitySets05d4af927f00e4d003d42058b5a420d4e33dae5f6caf1c7e3da0fae10a759678 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "05d4af927f00e4d003d42058b5a420d4e33dae5f6caf1c7e3da0fae10a759678"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "CodeFirstDatabase.T_CALL")
            {
                return GetView0();
            }

            if (extentName == "CodeFirstDatabase.T_EVENT")
            {
                return GetView1();
            }

            if (extentName == "CodeFirstDatabase.T_EVENT_TYPE")
            {
                return GetView2();
            }

            if (extentName == "MyModel.T_CALL")
            {
                return GetView3();
            }

            if (extentName == "MyModel.T_EVENT")
            {
                return GetView4();
            }

            if (extentName == "MyModel.T_EVENT_TYPE")
            {
                return GetView5();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.T_CALL.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_CALL
        [CodeFirstDatabaseSchema.T_CALL](T1.[T_CALL.RECORD_ID], T1.[T_CALL.CALLER], T1.[T_CALL.RECIEVER])
    FROM (
        SELECT 
            T.RECORD_ID AS [T_CALL.RECORD_ID], 
            T.CALLER AS [T_CALL.CALLER], 
            T.RECIEVER AS [T_CALL.RECIEVER], 
            True AS _from0
        FROM MyModel.T_CALL AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.T_EVENT.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_EVENT
        [CodeFirstDatabaseSchema.T_EVENT](T1.[T_EVENT.RECORD_ID], T1.[T_EVENT.RECORD_EVENT_ID], T1.[T_EVENT.RECORD_DATE], T1.[T_EVENT.CALL_ID])
    FROM (
        SELECT 
            T.RECORD_ID AS [T_EVENT.RECORD_ID], 
            T.RECORD_EVENT_ID AS [T_EVENT.RECORD_EVENT_ID], 
            T.RECORD_DATE AS [T_EVENT.RECORD_DATE], 
            T.CALL_ID AS [T_EVENT.CALL_ID], 
            True AS _from0
        FROM MyModel.T_EVENT AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.T_EVENT_TYPE.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView2()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_EVENT_TYPE
        [CodeFirstDatabaseSchema.T_EVENT_TYPE](T1.[T_EVENT_TYPE.EVENT_ID], T1.[T_EVENT_TYPE.EVENT_NAME], T1.[T_EVENT_TYPE.Description])
    FROM (
        SELECT 
            T.EVENT_ID AS [T_EVENT_TYPE.EVENT_ID], 
            T.EVENT_NAME AS [T_EVENT_TYPE.EVENT_NAME], 
            T.Description AS [T_EVENT_TYPE.Description], 
            True AS _from0
        FROM MyModel.T_EVENT_TYPE AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for MyModel.T_CALL.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView3()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_CALL
        [CallBook.T_CALL](T1.[T_CALL.RECORD_ID], T1.[T_CALL.CALLER], T1.[T_CALL.RECIEVER])
    FROM (
        SELECT 
            T.RECORD_ID AS [T_CALL.RECORD_ID], 
            T.CALLER AS [T_CALL.CALLER], 
            T.RECIEVER AS [T_CALL.RECIEVER], 
            True AS _from0
        FROM CodeFirstDatabase.T_CALL AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for MyModel.T_EVENT.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView4()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_EVENT
        [CallBook.T_EVENT](T1.[T_EVENT.RECORD_ID], T1.[T_EVENT.RECORD_EVENT_ID], T1.[T_EVENT.RECORD_DATE], T1.[T_EVENT.CALL_ID])
    FROM (
        SELECT 
            T.RECORD_ID AS [T_EVENT.RECORD_ID], 
            T.RECORD_EVENT_ID AS [T_EVENT.RECORD_EVENT_ID], 
            T.RECORD_DATE AS [T_EVENT.RECORD_DATE], 
            T.CALL_ID AS [T_EVENT.CALL_ID], 
            True AS _from0
        FROM CodeFirstDatabase.T_EVENT AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for MyModel.T_EVENT_TYPE.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView5()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing T_EVENT_TYPE
        [CallBook.T_EVENT_TYPE](T1.[T_EVENT_TYPE.EVENT_ID], T1.[T_EVENT_TYPE.EVENT_NAME], T1.[T_EVENT_TYPE.Description])
    FROM (
        SELECT 
            T.EVENT_ID AS [T_EVENT_TYPE.EVENT_ID], 
            T.EVENT_NAME AS [T_EVENT_TYPE.EVENT_NAME], 
            T.Description AS [T_EVENT_TYPE.Description], 
            True AS _from0
        FROM CodeFirstDatabase.T_EVENT_TYPE AS T
    ) AS T1");
        }
    }
}
