using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace GetBoolChoice
{
    /// <summary>
    /// <para type="synopsis">Module to ask boolean question to user</para>
    /// <para type="description">This function ask question to the user and wait his choice between 'y' ($true) and 'n' ($false).</para>
    /// <example>
    /// <para>#1 - Question without default value</para>
    /// <code>Get-BoolChoice("Do you want to continue ?")</code>
    /// </example>
    /// <example>
    /// <para>#2 - Question without default value</para>
    /// <code>Get-BoolChoice("Do you want to continue ?") -d "Y"</code>
    /// </example>
    /// <example>
    /// <para>#3 - Question text coming from pipeline</para>
    /// <code>"Do you want to continue ?" | Get-BoolChoice</code>
    /// </example>
    /// </summary>
    /// <para type="link" uri="https://github.com/toenn-vaot/psmodule-get-bool-choice">Source code</para>
    [Cmdlet(VerbsCommon.Get, "BoolChoice")]
    [OutputType(typeof(bool))]
    public class BoolChoiceCmdletCommand : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The question asked user</para>
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "What is the question you want to ask ?", ValueFromPipeline = true)]
        [Alias("q")]
        public string Question { get; set; }
        
        /// <summary>
        /// <para type="description">The default value used in case the user just press 'Enter' without passing any value</para>
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "What is the default value if user just hit Enter ?")]
        [Alias("d")]
        [ValidatePattern("[ynYN]")]
        public string DefaultValue { get; set; }
        
        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var yes = new ChoiceDescription("&Yes", "Yes");
            var no = new ChoiceDescription("&No", "No");
            var options = new Collection<ChoiceDescription>{ yes, no };
            var defaultValue = new Collection<string> { "y", "Y" }.Contains(DefaultValue) ? 0 : 1;

            var choice = Host.UI.PromptForChoice("", Question, options, defaultValue);
            WriteVerbose($"The bool choice is {choice}");

            WriteObject(choice == 0);
        }
    }
}
