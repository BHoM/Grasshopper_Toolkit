/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using Grasshopper.Kernel;
using BH.UI.Grasshopper.Templates;
using BH.UI.Templates;
using BH.UI.Components;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Parameters.Hints;
using BH.Engine.Grasshopper;

namespace BH.UI.Grasshopper.Components
{
    public class CreateCustomComponent : CallerComponent, IGH_VariableParameterComponent
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/

        public override Caller Caller { get; } = new CreateCustomCaller();


        /*******************************************/
        /**** Constructors                      ****/
        /*******************************************/

        public CreateCustomComponent() : base()
        {
            this.Params.ParameterChanged += (sender, e) => OnGrasshopperUpdates(sender, e.Parameter);
        }


        /*******************************************/
        /**** Public Methods                    ****/
        /*******************************************/

        public void OnGrasshopperUpdates(object sender, IGH_Param param)
        {
            if (sender == null)
                return;

            if (param == null)
                return;

            CreateCustomCaller caller = Caller as CreateCustomCaller;
            if (caller == null)
                return;

            RecordUndoEvent("CreateCustom.OnGrasshopperUpdates");
            // Updating Caller.InputParams based on the new Grasshopper parameter just received
            switch (sender)
            {
                case "CreateParameter":
                    caller.AddInput(param.NickName, param.Type());
                    return;
                case "DestroyParameter":
                    caller.RemoveInput(param.NickName);
                    return;
                default:
                    // Fired when TypeHint or Access change.
                    // We update the caller with the new type and let SolveIntance set the new Accessor
                    int index = Params.IndexOfInputParam(param.Name);
                    if (index != -1)
                    {
                        caller.UpdateInput(index, param.NickName, param.Type(caller));
                        ExpireSolution(true); // If only NickName has changed grasshopper does not recompute the solution, so we explicitly do it
                    }
                    return;
            }
        }


        /*******************************************/
        /**** Override Methods                  ****/
        /*******************************************/

        public override bool CanInsertParameter(GH_ParameterSide side, int index)
        {
            return side == GH_ParameterSide.Input;
        }

        /*******************************************/

        public override bool CanRemoveParameter(GH_ParameterSide side, int index)
        {
            return side == GH_ParameterSide.Input;
        }

        /*******************************************/

        public override IGH_Param CreateParameter(GH_ParameterSide side, int index)
        {
            Param_ScriptVariable param = new Param_ScriptVariable
            {
                NickName = GH_ComponentParamServer.InventUniqueNickname("xyzuvw", this.Params.Input),
                TypeHint = new GH_NullHint()
            };
            this.OnGrasshopperUpdates("CreateParameter", param);
            return param;
        }

        /*******************************************/

        public override bool DestroyParameter(GH_ParameterSide side, int index)
        {
            if (side == GH_ParameterSide.Output)
                return true;

            if (Params.Input.Count <= index)
                return true;

            this.OnGrasshopperUpdates("DestroyParameter", Params.Input[index]);
            return true;
        }

        /*******************************************/

        public override void VariableParameterMaintenance()
        {
            foreach (IGH_Param param in Params.Input)
            {
                Param_ScriptVariable paramScript = param as Param_ScriptVariable;
                if (paramScript != null)
                {
                    paramScript.ShowHints = true;
                    paramScript.Hints = Engine.Grasshopper.Query.AvailableHints;
                    paramScript.AllowTreeAccess = true;
                }
            }
        }

        /*******************************************/
    }
}
